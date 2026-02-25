using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    internal interface IColumnNameMapper
    {
        ColumnNameIndex GetColumn(string mappedColumnName);

        ColumnNameIndex GetColumn(int mappedColumnIndex);

        ColumnNameIndex GetMappedColumn(string columnName);

        ColumnNameIndex GetMappedColumn(int columnIndex);

        string[] GetSortedColumnNames();

        string[] GetSortedMappedColumnNames();
    }

    internal class ColumnNameIndex : IEquatable<ColumnNameIndex>
    {
        public string ColumnName { get; }

        public int Index { get; }

        public ColumnNameIndex(string columnName, int index)
        {
            ColumnName = columnName;
            Index = index;
        }

        public bool Equals(ColumnNameIndex? other)
        {
            return other != null && ColumnName == other.ColumnName && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            return obj is ColumnNameIndex cni && Equals(cni);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ColumnName, Index);
        }

        public override string ToString()
        {
            return $"{nameof(ColumnName)}: {ColumnName}, {nameof(Index)}: {Index}";
        }
    }

    internal class ColumnNameMapper : IColumnNameMapper
    {
        // The key is the tranformed column name and sorted index. The value is the original column name and the original index.
        private readonly Dictionary<ColumnNameIndex, ColumnNameIndex> _columnNameMap = new Dictionary<ColumnNameIndex, ColumnNameIndex>();

        public ColumnNameMapper(IEnumerable<string> columnNames, INameTransform? transform = null, IColumnOrderSorter? sorter = null)
        {
            _columnNameMap = Map(columnNames, transform ?? new PassThruNameTransform(), sorter ?? ColumnOrderSorter.Default);
        }

        private static Dictionary<ColumnNameIndex, ColumnNameIndex> Map(IEnumerable<string> columnNames, INameTransform? transform, IColumnOrderSorter? sorter)
        {
            if (!columnNames.Any())
            {
                return new Dictionary<ColumnNameIndex, ColumnNameIndex>();
            }

            transform ??= new PassThruNameTransform();
            sorter ??= ColumnOrderSorter.Default;

            var transformedNames = new Dictionary<string, ColumnNameIndex>();
            var idx = 0;

            foreach (var columnName in columnNames)
            {
                var transformed = transform.Apply(columnName);
                transformedNames.Add(transformed, new ColumnNameIndex(columnName, idx++));
            }

            var sortedTransformedNames = sorter.Sort(transformedNames.Select(kv => kv.Key)).ToList();

            var map = new Dictionary<ColumnNameIndex, ColumnNameIndex>();

            for (var i = 0; i < sortedTransformedNames.Count; i++)
            {
                var sortedTransformedColumnName = sortedTransformedNames[i];

                if (!transformedNames.TryGetValue(sortedTransformedColumnName, out var original))
                {
                    throw new InvalidOperationException($"Transformed column name '{sortedTransformedColumnName}' not found in original names.");
                }

                map.Add(new ColumnNameIndex(sortedTransformedColumnName, i), original);
            }

            return map;
        }

        public ColumnNameIndex GetColumn(string mappedColumnName)
        {
            foreach (var kvp in _columnNameMap)
            {
                if (kvp.Key.ColumnName == mappedColumnName)
                {
                    return kvp.Value;
                }
            }

            throw new KeyNotFoundException($"Mapped column name '{mappedColumnName}' not found.");
        }

        public ColumnNameIndex GetColumn(int mappedColumnIndex)
        {
            foreach (var kvp in _columnNameMap)
            {
                if (kvp.Key.Index == mappedColumnIndex)
                {
                    return kvp.Value;
                }
            }

            throw new KeyNotFoundException($"Mapped column index '{mappedColumnIndex}' not found.");
        }

        public ColumnNameIndex GetMappedColumn(string columnName)
        {
            foreach (var kvp in _columnNameMap)
            {
                if (kvp.Value.ColumnName == columnName)
                {
                    return kvp.Key;
                }
            }

            throw new KeyNotFoundException($"Column name '{columnName}' not found.");
        }

        public ColumnNameIndex GetMappedColumn(int columnIndex)
        {
            foreach (var kvp in _columnNameMap)
            {
                if (kvp.Value.Index == columnIndex)
                {
                    return kvp.Key;
                }
            }

            throw new KeyNotFoundException($"Column index '{columnIndex}' not found.");
        }

        public string[] GetSortedColumnNames()
        {
            var result = new string[_columnNameMap.Count];

            foreach (var kvp in _columnNameMap)
            {
                result[kvp.Key.Index] = kvp.Value.ColumnName;
            }

            return result;
        }

        public string[] GetSortedMappedColumnNames()
        {
            var result = new string[_columnNameMap.Count];

            foreach (var kvp in _columnNameMap)
            {
                result[kvp.Key.Index] = kvp.Key.ColumnName;
            }

            return result;
        }
    }
}
