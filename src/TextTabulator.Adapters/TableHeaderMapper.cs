using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    internal interface ITableHeaderMapper
    {
        int HeaderCount { get; }

        TableHeader GetHeader(string mappedHeaderName);

        TableHeader GetHeader(int mappedHeaderIndex);

        TableHeader GetMappedHeader(string headerName);

        TableHeader GetMappedHeader(int headerIndex);

        string[] GetSortedHeaderNames();

        string[] GetSortedMappedHeaderNames();
    }

    internal class TableHeaderMapper : ITableHeaderMapper
    {
        // The key is the tranformed header name and sorted index. The value is the original header name and the original index.
        private readonly Dictionary<TableHeader, TableHeader> _headerMap = new Dictionary<TableHeader, TableHeader>();

        public int HeaderCount => _headerMap.Count;

        public TableHeaderMapper(IEnumerable<string> headerNames, INameTransform? transform = null, IHeaderOrderSorter? sorter = null)
        {
            _headerMap = Map(headerNames, transform ?? new PassThruNameTransform(), sorter ?? HeaderOrderSorter.Default);
        }

        private static Dictionary<TableHeader, TableHeader> Map(IEnumerable<string> headerNames, INameTransform? transform, IHeaderOrderSorter? sorter)
        {
            if (!headerNames.Any())
            {
                return new Dictionary<TableHeader, TableHeader>();
            }

            transform ??= new PassThruNameTransform();
            sorter ??= HeaderOrderSorter.Default;

            var transformedNames = new Dictionary<string, TableHeader>();
            var idx = 0;

            foreach (var headerName in headerNames)
            {
                var transformed = transform.Apply(headerName);
                transformedNames.Add(transformed, new TableHeader(headerName, idx++));
            }

            var sortedTransformedNames = sorter.Sort(transformedNames.Select(kv => kv.Key)).ToList();

            var map = new Dictionary<TableHeader, TableHeader>();

            for (var i = 0; i < sortedTransformedNames.Count; i++)
            {
                var sortedTransformedHeaderName = sortedTransformedNames[i];

                if (!transformedNames.TryGetValue(sortedTransformedHeaderName, out var original))
                {
                    throw new InvalidOperationException($"Transformed header name '{sortedTransformedHeaderName}' not found in original names.");
                }

                map.Add(new TableHeader(sortedTransformedHeaderName, i), original);
            }

            return map;
        }

        public TableHeader GetHeader(string mappedHeaderName)
        {
            foreach (var kvp in _headerMap)
            {
                if (kvp.Key.Name == mappedHeaderName)
                {
                    return kvp.Value;
                }
            }

            throw new KeyNotFoundException($"Mapped column name '{mappedHeaderName}' not found.");
        }

        public TableHeader GetHeader(int mappedHeaderIndex)
        {
            foreach (var kvp in _headerMap)
            {
                if (kvp.Key.Index == mappedHeaderIndex)
                {
                    return kvp.Value;
                }
            }

            throw new KeyNotFoundException($"Mapped column index '{mappedHeaderIndex}' not found.");
        }

        public TableHeader GetMappedHeader(string columnName)
        {
            foreach (var kvp in _headerMap)
            {
                if (kvp.Value.Name == columnName)
                {
                    return kvp.Key;
                }
            }

            throw new KeyNotFoundException($"Header name '{columnName}' not found.");
        }

        public TableHeader GetMappedHeader(int columnIndex)
        {
            foreach (var kvp in _headerMap)
            {
                if (kvp.Value.Index == columnIndex)
                {
                    return kvp.Key;
                }
            }

            throw new KeyNotFoundException($"Header index '{columnIndex}' not found.");
        }

        public string[] GetSortedHeaderNames()
        {
            var result = new string[_headerMap.Count];

            foreach (var kvp in _headerMap)
            {
                result[kvp.Key.Index] = kvp.Value.Name;
            }

            return result;
        }

        public string[] GetSortedMappedHeaderNames()
        {
            var result = new string[_headerMap.Count];

            foreach (var kvp in _headerMap)
            {
                result[kvp.Key.Index] = kvp.Key.Name;
            }

            return result;
        }
    }
}
