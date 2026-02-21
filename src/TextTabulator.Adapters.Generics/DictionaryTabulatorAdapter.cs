
namespace TextTabulator.Adapters.Generics
{
    public interface IDictionaryTabulatorAdapter<TKey, TValue> : ITabulatorAdapter
    {
    }

    public class DictionaryTabulatorAdapter<TKey, TValue> : IDictionaryTabulatorAdapter<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly DictionaryTabulatorAdapterOptions _options;

        private readonly Dictionary<string, int> _keyIndexMap = new();

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
                headers.Add("Key");
                headers.Add("Value");
                _keyIndexMap.Add("Key", 0);
                _keyIndexMap.Add("Value", 1);
            }
            else
            {
                var reflector = new Reflector(type);

                var propertyHeaders = new List<string>();
                var propertyInfos = reflector.GetPropertyInfos();

                foreach (var propertyInfo in propertyInfos)
                {
                    propertyHeaders.Add(_options.HeaderNameTransform.Apply(propertyInfo.Name));
                }

                var fieldHeaders = new List<string>();
                var fieldInfos = reflector.GetFieldInfos();

                foreach (var fieldInfo in fieldInfos)
                {
                    fieldHeaders.Add(_options.HeaderNameTransform.Apply(fieldInfo.Name));
                }

                headers.AddRange(propertyHeaders);
                headers.AddRange(fieldHeaders);

                if (_options.ColumnSortOrder == SortOrder.AlphaNumericAscending)
                {
                    headers = headers.Order().ToList();
                }
                else if (_options.ColumnSortOrder == SortOrder.AlphaNumericDescending)
                {
                    headers = headers.OrderDescending().ToList();
                }

                headers.Insert(0, "Key");

                for (var i = 0; i < headers.Count; i++)
                {
                    _keyIndexMap.Add(headers[i], i);
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