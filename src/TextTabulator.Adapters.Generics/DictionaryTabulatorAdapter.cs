
namespace TextTabulator.Adapters.Generics
{
    public interface IDictionaryTabulatorAdapter<TKey, TValue> : ITabulatorAdapter
    {
    }

    public class DictionaryTabulatorAdapter<TKey, TValue> : IDictionaryTabulatorAdapter<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly IDictionaryTabulatorAdapterOptions _options;

        private readonly Dictionary<string, int> _keyIndexMap = new();

        public DictionaryTabulatorAdapter(IDictionary<TKey, TValue> dictionary, IDictionaryTabulatorAdapterOptions options)
        {
            _dictionary = dictionary;
            _options = options;
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            var headers = new List<string>();
            headers.Add("Key");

            _keyIndexMap.Clear();

            if (_options.AxesOrientation == AxesOrientation.Default)
            {
                var type = _dictionary.GetType().GetGenericArguments()[1];

                if (PrimitiveLike.Detect(type))
                {
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

                    headers.AddRange(propertyHeaders.Order());
                    headers.AddRange(fieldHeaders.Order());

                    for (var i = 0; i < headers.Count; i++)
                    {
                        _keyIndexMap.Add(headers[i], i);
                    }
                }
            }

            return headers;
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var rows = new List<IEnumerable<string>>();

            if (_options.AxesOrientation == AxesOrientation.Default)
            {
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
            }
            //else
            //{
            //    var reflector = new Reflector(_dictionary.GetType().GetGenericArguments()[1]);

            //    var propertyInfos = reflector.GetPropertyInfos();
            //    var fieldInfos = reflector.GetFieldInfos();
            //    var rows = new List<List<string>>();

            //    for (var i = 0; i < _dictionary.Count; i++)
            //    {
            //        var kvp = _dictionary.ElementAt(i);

            //        for (var j = 0; j < propertyInfos.Length; j++)
            //        {
            //            var value = propertyInfos[j].GetValue(kvp.Value)?.ToString() ?? string.Empty;

            //            if (i >= rows.Count)
            //            {
            //                rows.Add(new List<string>());
            //            }

            //            rows[i].Add(value);
            //        }

            //        for (var j = 0; j < fieldInfos.Length; j++)
            //        {
            //            var value = fieldInfos[j].GetValue(kvp.Value)?.ToString() ?? string.Empty;

            //            if (propertyInfos.Length + i >= rows.Count)
            //            {
            //                rows.Add(new List<string>());
            //            }

            //            rows[i].Add(value);
            //        }
            //    }
            //}

            return rows;
        }
    }
}