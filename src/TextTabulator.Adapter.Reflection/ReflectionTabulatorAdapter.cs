using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TextTabulator.Adapter.Reflection
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
    /// Class that implements the ITabulatorAdapter interface in order to adapt an enumeration to be consumed by Tabulator.Tabulate.
    /// The type 'T' will be reflected and the names of its properties and/or fields will be adapted to headers. The values contained
    /// in each object in the enumeration will be adapted to the row values.
    /// </summary>
    /// <typeparam name="T">Type that will be reflected.</typeparam>
    public class ReflectionTabulatorAdapter<T> : IReflectionTabulatorAdapter
    {
        private readonly IEnumerable<T> _items;
        private readonly TypeMembers _typeMembers;
        private readonly AccessModifiers _accessModifiers;

        private PropertyInfo[]? _propertyInfos;
        private FieldInfo[]? _fieldInfos;

        public ReflectionTabulatorAdapter(IEnumerable<T> items, TypeMembers typeMembers = TypeMembers.Properties, AccessModifiers accessModifiers = AccessModifiers.Public)
        {
            _items = items;
            _typeMembers = typeMembers;
            _accessModifiers = accessModifiers;
        }

        private void GetMemberInfos()
        {
            Type? type = null;
            var bindingFlags = BindingFlags.Instance
                | ((_accessModifiers & AccessModifiers.Public) == 0 ? 0 : BindingFlags.Public)
                | ((_accessModifiers & AccessModifiers.NonPublic) == 0 ? 0 : BindingFlags.NonPublic);

            if ((_typeMembers & TypeMembers.Properties) != 0 && _propertyInfos == null)
            {
                type ??= typeof(T);
                _propertyInfos = type.GetProperties(bindingFlags);
            }

            if ((_typeMembers & TypeMembers.Fields) != 0 && _fieldInfos == null)
            {
                type ??= typeof(T);
                // Ignore backing fields.
                _fieldInfos = type.GetFields(bindingFlags).Where(i => !i.Name.StartsWith("<") || !i.Name.Contains(">k__BackingField")).ToArray();
            }
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            GetMemberInfos();

            var headers = new List<string>();

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

            return headers.Count == 0 ? null : headers;
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            GetMemberInfos();

            if ((_propertyInfos == null || _propertyInfos.Length == 0)
                && (_fieldInfos == null || _fieldInfos.Length == 0))
            {
                return Array.Empty<IEnumerable<string>>();
            }

            var values = new List<string[]>();

            foreach (var item in _items)
            {
                var propertyCount = _propertyInfos?.Length ?? 0;
                var fieldCount = _fieldInfos?.Length ?? 0;

                var row = new string[propertyCount + fieldCount];

                if (_propertyInfos != null)
                {
                    for (var i = 0; i < propertyCount; i++)
                    {
                        row[i] = _propertyInfos[i].GetValue(item).ToString();
                    }
                }

                if (_fieldInfos != null)
                {
                    for (var i = propertyCount; i < fieldCount; i++)
                    {
                        row[i] = _fieldInfos[i].GetValue(item).ToString();
                    }
                }

                values.Add(row);
            }

            return values;
        }
    }
}
