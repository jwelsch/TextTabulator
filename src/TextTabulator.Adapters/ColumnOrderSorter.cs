using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    /// <summary>
    /// Sorts column the columns themselves in a specific order.
    /// </summary>
    public interface IColumnOrderSorter
    {
        IEnumerable<string> Sort(IEnumerable<string> columnNames, Func<IEnumerable<string>, IEnumerable<string>> sorter);
    }

    public class CustomColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames, Func<IEnumerable<string>, IEnumerable<string>> sorter) => sorter(columnNames);
    }

    public class DefaultColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames, Func<IEnumerable<string>, IEnumerable<string>> sorter) => sorter(columnNames);
    }

    public class AscendingColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames, Func<IEnumerable<string>, IEnumerable<string>> sorter) => sorter(columnNames.OrderBy(c => c));
    }

    public class DescendingColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames, Func<IEnumerable<string>, IEnumerable<string>> sorter) => sorter(columnNames.OrderByDescending(c => c));
    }
}
