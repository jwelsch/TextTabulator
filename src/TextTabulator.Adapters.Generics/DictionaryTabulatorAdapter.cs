using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters.Generics
{
    /// <summary>
    /// Public interface for IDictionaryTabulatorAdapter<TKey,TValue>.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public interface IDictionaryTabulatorAdapter<TKey, TValue> : ITabulatorAdapter
    {
    }

    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt IDictionary<TKey,TValue> to be consumed by the Tabulator.Tabulate method.
    /// Each key in the dictionary will be adapted to a row, and the properties and/or fields of the value type will be adapted to columns.
    /// The names of the properties and/or fields of the value type will be adapted to headers.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class DictionaryTabulatorAdapter<TKey, TValue> : IDictionaryTabulatorAdapter<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly DictionaryTabulatorAdapterOptions _options;

        private readonly Dictionary<string, int> _keyIndexMap = new Dictionary<string, int>();

        public DictionaryTabulatorAdapter(IDictionary<TKey, TValue> dictionary, DictionaryTabulatorAdapterOptions? options = null)
        {
            _dictionary = dictionary;
            _options = options ?? new DictionaryTabulatorAdapterOptions();
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            var headers = new List<string>();

            _keyIndexMap.Clear();

            var type = _dictionary.GetType().GetGenericArguments()[1];

            if (PrimitiveLike.Detect(type))
            {
                headers.Add(_options.HeaderNameTransform.Apply("Key"));
                headers.Add(_options.HeaderNameTransform.Apply("Value"));
                _keyIndexMap.Add("Key", 0);
                _keyIndexMap.Add("Value", 1);
            }
            else
            {
                var reflector = new Reflector(type);

                var nameTransformMap = new Dictionary<string, string>();

                var propertyHeaders = new List<string>();
                var propertyInfos = reflector.GetPropertyInfos();

                foreach (var propertyInfo in propertyInfos)
                {
                    var transformedName = _options.HeaderNameTransform.Apply(propertyInfo.Name);
                    propertyHeaders.Add(transformedName);
                    nameTransformMap[transformedName] = propertyInfo.Name;
                }

                var fieldHeaders = new List<string>();
                var fieldInfos = reflector.GetFieldInfos();

                foreach (var fieldInfo in fieldInfos)
                {
                    var transformedName = _options.HeaderNameTransform.Apply(fieldInfo.Name);
                    fieldHeaders.Add(transformedName);
                    nameTransformMap[transformedName] = fieldInfo.Name;
                }

                headers.AddRange(propertyHeaders);
                headers.AddRange(fieldHeaders);

                if (_options.ColumnSortOrder == SortOrder.AlphaNumericAscending)
                {
                    headers = headers.OrderBy(h => h).ToList();
                }
                else if (_options.ColumnSortOrder == SortOrder.AlphaNumericDescending)
                {
                    headers = headers.OrderByDescending(h => h).ToList();
                }

                headers.Insert(0, _options.HeaderNameTransform.Apply("Key"));
                _keyIndexMap.Add("Key", 0);

                // Start from 1 since 0 is reserved for the "Key" column.
                for (var i = 1; i < headers.Count; i++)
                {
                    _keyIndexMap.Add(nameTransformMap[headers[i]], i);
                }
            }

            return headers;
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var rows = new List<IEnumerable<string>>();
            var type = _dictionary.GetType().GetGenericArguments()[1];
            var reflector = new Reflector(type);

            foreach (var kvp in _dictionary)
            {
                var row = (string[])Array.CreateInstance(typeof(string), _keyIndexMap.Count);

                row[0] = kvp.Key?.ToString() ?? string.Empty;

                if (PrimitiveLike.Detect(type))
                {
                    row[1] = kvp.Value?.ToString() ?? string.Empty;
                }
                else
                {
                    var propertyInfos = reflector.GetPropertyInfos();

                    for (var i = 0; i < propertyInfos.Length; i++)
                    {
                        var value = propertyInfos[i].GetValue(kvp.Value)?.ToString() ?? string.Empty;
                        row[_keyIndexMap[propertyInfos[i].Name]] = value;
                    }

                    var fieldInfos = reflector.GetFieldInfos();

                    for (var i = 0; i < fieldInfos.Length; i++)
                    {
                        var value = fieldInfos[i].GetValue(kvp.Value)?.ToString() ?? string.Empty;
                        row[_keyIndexMap[fieldInfos[i].Name]] = value;
                    }
                }

                rows.Add(row);
            }

            return rows;
        }
    }
}