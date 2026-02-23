using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    public interface IColumnNameMapper
    {
        ColumnNameIndex GetColumnName(string mappedColumnName);

        ColumnNameIndex GetMappedColumnName(string columnName);

        string[] GetSortedColumnNames();

        string[] GetSortedMappedColumnNames();
    }

    public class ColumnNameIndex
    {
        public string ColumnName { get; }

        public int SortedIndex { get; }

        public ColumnNameIndex(string columnName, int sortedIndex)
        {
            ColumnName = columnName;
            SortedIndex = sortedIndex;
        }
    }

    public class ColumnNameMapper : IColumnNameMapper
    {
        // The key is the tranformed column name. The value is the original column name and the sorted index.
        private readonly Dictionary<string, ColumnNameIndex> _columnNameMap = new Dictionary<string, ColumnNameIndex>();

        public ColumnNameMapper(IEnumerable<string> columnNames, INameTransform? transform = null, IColumnOrderSorter? sorter = null)
        {
            _columnNameMap = Map(columnNames, transform ?? new PassThruNameTransform(), sorter ?? ColumnOrderSorter.Default);
        }

        private static Dictionary<string, ColumnNameIndex> Map(IEnumerable<string> columnNames, INameTransform? transform, IColumnOrderSorter? sorter)
        {
            if (columnNames == null) throw new ArgumentNullException(nameof(columnNames));

            transform ??= new PassThruNameTransform();
            sorter ??= ColumnOrderSorter.Default;

            var transformedNames = new List<KeyValuePair<string, string>>();

            foreach (var columnName in columnNames)
            {
                var transformed = transform.Apply(columnName);
                transformedNames.Add(new KeyValuePair<string, string>(columnName, transformed));
            }

            var sortedTransformedNames = sorter.Sort(transformedNames.Select(kv => kv.Value)).ToList();

            var map = new Dictionary<string, ColumnNameIndex>();

            for (var i = 0; i < sortedTransformedNames.Count; i++)
            {
                map.Add(sortedTransformedNames[i], new ColumnNameIndex(transformedNames.First(n => n.Value == sortedTransformedNames[i]).Key, i));
            }

            return map;
        }

        public ColumnNameIndex GetColumnName(string mappedColumnName)
        {
            if (_columnNameMap.TryGetValue(mappedColumnName, out var result))
            {
                return result;
            }

            throw new KeyNotFoundException($"Mapped column name '{mappedColumnName}' not found.");
        }

        public ColumnNameIndex GetMappedColumnName(string columnName)
        {
            foreach (var kvp in _columnNameMap)
            {
                if (kvp.Value.ColumnName == columnName)
                {
                    return new ColumnNameIndex(kvp.Key, kvp.Value.SortedIndex);
                }
            }

            throw new KeyNotFoundException($"Column name '{columnName}' not found.");
        }

        public string[] GetSortedColumnNames()
        {
            return _columnNameMap.OrderBy(kvp => kvp.Value.SortedIndex).Select(kvp => kvp.Value.ColumnName).ToArray();
        }

        public string[] GetSortedMappedColumnNames()
        {
            return _columnNameMap.OrderBy(kvp => kvp.Value.SortedIndex).Select(kvp => kvp.Key).ToArray();
        }
    }
}
