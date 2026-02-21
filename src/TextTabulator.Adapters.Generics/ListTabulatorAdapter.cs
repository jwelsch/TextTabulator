namespace TextTabulator.Adapters.Generics
{
    /// <summary>
    /// Public interface for IListTabulatorAdapter<T>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IListTabulatorAdapter<T> : ITabulatorAdapter
    {
    }

    /// <summary>
    /// Class that implements the ITabulatorAdapter interface in order to adapt IList<T> to be consumed by the Tabulator.Tabulate method.
    /// Each item in the list will be adapted to a row, and the properties and/or fields of the item type will be adapted to columns.
    /// The names of the properties and/or fields of the item type will be adapted to headers.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public class ListTabulatorAdapter<T> : IListTabulatorAdapter<T>
    {
        private readonly IList<T> _list;
        private readonly ListTabulatorAdapterOptions _options;

        private readonly Dictionary<string, int> _keyIndexMap = new();

        public ListTabulatorAdapter(IList<T> list, ListTabulatorAdapterOptions? options = null)
        {
            _list = list;
            _options = options ?? new ListTabulatorAdapterOptions();
        }

        public IEnumerable<string>? GetHeaderStrings()
        {
            var headers = new List<string>();

            _keyIndexMap.Clear();

            var type = typeof(T);

            if (PrimitiveLike.Detect(type))
            {
                headers.Add(_options.HeaderNameTransform.Apply("Value"));
                _keyIndexMap.Add("Value", 0);
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
                    nameTransformMap[transformedName] = propertyInfo.Name;
                    propertyHeaders.Add(transformedName);
                }

                var fieldHeaders = new List<string>();
                var fieldInfos = reflector.GetFieldInfos();

                foreach (var fieldInfo in fieldInfos)
                {
                    var transformedName = _options.HeaderNameTransform.Apply(fieldInfo.Name);
                    nameTransformMap[transformedName] = fieldInfo.Name;
                    fieldHeaders.Add(transformedName);
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

                for (var i = 0; i < headers.Count; i++)
                {
                    _keyIndexMap.Add(nameTransformMap[headers[i]], i);
                }
            }

            return headers;
        }

        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            var rows = new List<IEnumerable<string>>();
            var type = typeof(T);
            var reflector = new Reflector(type);

            foreach (var item in _list)
            {
                var row = (string[])Array.CreateInstance(typeof(string), _keyIndexMap.Count);

                if (PrimitiveLike.Detect(type))
                {
                    row[0] = item?.ToString() ?? string.Empty;
                }
                else
                {
                    var propertyInfos = reflector.GetPropertyInfos();

                    for (var i = 0; i < propertyInfos.Length; i++)
                    {
                        var value = propertyInfos[i].GetValue(item)?.ToString() ?? string.Empty;
                        row[_keyIndexMap[propertyInfos[i].Name]] = value;
                    }

                    var fieldInfos = reflector.GetFieldInfos();

                    for (var i = 0; i < fieldInfos.Length; i++)
                    {
                        var value = fieldInfos[i].GetValue(item)?.ToString() ?? string.Empty;
                        row[_keyIndexMap[fieldInfos[i].Name]] = value;
                    }
                }

                rows.Add(row);
            }

            return rows;
        }
    }
}
