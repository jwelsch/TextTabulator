using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TextTabulator.Adapters.Reflection
{
    /// <summary>
    /// Specifies which category of members. This enumeration supports bitwise operations.
    /// </summary>
    [Flags]
    public enum TypeMembers : byte
    {
        /// <summary>
        /// Property members.
        /// </summary>
        Properties = 0x01,

        /// <summary>
        /// Field members, not including backing fields.
        /// </summary>
        Fields = 0x10,

        /// <summary>
        /// Both field and property members.
        /// </summary>
        PropertiesAndFields = Properties | Fields,
    }

    /// <summary>
    /// Specifies which access modifiers. This enumeration supports bitwise operations.
    /// </summary>
    [Flags]
    public enum AccessModifiers : byte
    {
        /// <summary>
        /// Members with the public access modifier.
        /// </summary>
        Public = 0x01,

        /// <summary>
        /// Members with a non-public access modifier.
        /// </summary>
        NonPublic = 0x10,

        /// <summary>
        /// Members with both public and non-public access modifiers.
        /// </summary>
        PublicAndNonPublic = Public | NonPublic,
    }

    /// <summary>
    /// Public interface for IReflectionTabulatorAdapter.
    /// </summary>
    public interface IReflectionTabulatorAdapter : ITabulatorAdapter
    {
    }

    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt types to be consumed by the Tabulator.Tabulate method.
    /// The type 'T' will be reflected and the names of its properties and/or fields will be adapted to headers. The values contained
    /// in each object in the enumeration will be adapted to the row values.
    /// </summary>
    /// <typeparam name="T">Type that will be reflected.</typeparam>
    public class ReflectionTabulatorAdapter<T> : IReflectionTabulatorAdapter
    {
        private readonly IEnumerable<T> _items;
        private readonly int _itemCount;
        private readonly ReflectionTabulatorAdapterOptions _options;

        private PropertyInfo[]? _propertyInfos;
        private FieldInfo[]? _fieldInfos;
        private TableHeaderMapper? _mapper;

        /// <summary>
        /// ReflectionTabulatorAdapter constructor that takes an enumerable.
        /// </summary>
        /// <param name="items">Enumerable of items to adapt.</param>
        /// <param name="options">Options for the adapter.</param>
        public ReflectionTabulatorAdapter(IEnumerable<T> items, ReflectionTabulatorAdapterOptions? options = null)
        {
            _items = items;
            _itemCount = _items.Count();
            _options = options ?? new ReflectionTabulatorAdapterOptions();
        }

        /// <summary>
        /// ReflectionTabulatorAdapter constructor that takes an enumerable.
        /// </summary>
        /// <param name="item">Item to adapt.</param>
        /// <param name="options">Options for the adapter.</param>
        public ReflectionTabulatorAdapter(T item, ReflectionTabulatorAdapterOptions? options = null)
            : this(new T[] { item }, options)
        {
        }

        private void GetMemberInfos()
        {
            if (_fieldInfos != null && _propertyInfos != null)
            {
                return;
            }

            var type = typeof(T);
            var reflector = new Reflector(type);
            var bindingFlags = BindingFlags.Instance
                | ((_options.AccessModifiers & AccessModifiers.Public) == 0 ? 0 : BindingFlags.Public)
                | ((_options.AccessModifiers & AccessModifiers.NonPublic) == 0 ? 0 : BindingFlags.NonPublic);

            if ((_options.TypeMembers & TypeMembers.Properties) != 0 && _propertyInfos == null)
            {
                _propertyInfos = reflector.GetPropertyInfos(bindingFlags);
            }

            if ((_options.TypeMembers & TypeMembers.Fields) != 0 && _fieldInfos == null)
            {
                _fieldInfos = reflector.GetFieldInfos(bindingFlags);
            }
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            GetMemberInfos();

            var headers = new List<string>();

            if (_options.AxesOrientation == AxesOrientation.Default)
            {
                if (_propertyInfos != null)
                {
                    for (var i = 0; i < _propertyInfos!.Length; i++)
                    {
                        headers.Add(_propertyInfos[i].Name);
                    }
                }

                if (_fieldInfos != null)
                {
                    for (var i = 0; i < _fieldInfos!.Length; i++)
                    {
                        headers.Add(_fieldInfos[i].Name);
                    }
                }

                _mapper = new TableHeaderMapper(headers, _options.MemberNameTransform, _options.HeaderSorter);

                headers = _mapper.GetSortedHeaderNames().ToList();
            }
            else
            {
                if (_propertyInfos != null || _fieldInfos != null)
                {
                    headers.Add("Member");

                    if (_itemCount == 1)
                    {
                        headers.Add("Value");
                    }
                    else
                    {
                        for (var i = 0; i < _itemCount; i++)
                        {
                            headers.Add($"Value{i}");
                        }
                    }
                }
            }

            return headers.Count == 0 ? null : headers;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            GetMemberInfos();

            if ((_propertyInfos == null || _propertyInfos.Length == 0)
                && (_fieldInfos == null || _fieldInfos.Length == 0))
            {
                return Array.Empty<IEnumerable<string>>();
            }

            var values = new List<string[]>();

            if (_options.AxesOrientation == AxesOrientation.Default)
            {
                foreach (var item in _items)
                {
                    var propertyCount = _propertyInfos?.Length ?? 0;
                    var fieldCount = _fieldInfos?.Length ?? 0;

                    var row = new string[propertyCount + fieldCount];

                    if (_propertyInfos != null)
                    {
                        for (var i = 0; i < propertyCount; i++)
                        {
                            var propertyInfo = _propertyInfos[i];
                            var rowIndex = _mapper?.GetMappedHeader(propertyInfo.Name).Index ?? i;
                            row[rowIndex] = _options.TypeFormatter.FormatTypeValue(propertyInfo.GetValue(item));
                        }
                    }

                    if (_fieldInfos != null)
                    {
                        for (var i = propertyCount; i < fieldCount + propertyCount; i++)
                        {
                            var fieldInfo = _fieldInfos[i - propertyCount];
                            var rowIndex = _mapper?.GetMappedHeader(fieldInfo.Name).Index ?? i;
                            row[rowIndex] = _options.TypeFormatter.FormatTypeValue(fieldInfo.GetValue(item));
                        }
                    }

                    values.Add(row);
                }
            }
            else
            {
                var memberCount = (_propertyInfos?.Length ?? 0) + (_fieldInfos?.Length ?? 0);

                for (var i = 0; i < memberCount; i++)
                {
                    var row = new string[_itemCount + 1];

                    if (_propertyInfos != null && i < _propertyInfos.Length)
                    {
                        row[0] = _propertyInfos[i].Name;
                        for (var j = 0; j < _itemCount; j++)
                        {
                            row[j + 1] = _options.TypeFormatter.FormatTypeValue(_propertyInfos[i].GetValue(_items.ElementAt(j)));
                        }
                    }
                    else if (_fieldInfos != null)
                    {
                        var fieldIndex = i - (_propertyInfos?.Length ?? 0);
                        row[0] = _fieldInfos[fieldIndex].Name;
                        for (var j = 0; j < _itemCount; j++)
                        {
                            row[j + 1] = _options.TypeFormatter.FormatTypeValue(_fieldInfos[fieldIndex].GetValue(_items.ElementAt(j)));
                        }
                    }

                    values.Add(row);
                }
            }

            return values;
        }
    }
}
