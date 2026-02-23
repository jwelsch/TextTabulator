using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    /// <summary>
    /// Interface for sorting the columns themselves in a specific order.
    /// </summary>
    public interface IColumnOrderSorter
    {
        /// <summary>
        /// Called to sort the column names.
        /// </summary>
        /// <param name="columnNames">The column names to sort.</param>
        /// <returns>The sorted column names.</returns>
        IEnumerable<string> Sort(IEnumerable<string> columnNames);
    }

    /// <summary>
    /// A column order sorter. Use the static members for built-in sorting options, or create a new instance with a custom sorting function.
    /// </summary>
    public class ColumnOrderSorter : IColumnOrderSorter
    {
        /// <summary>
        /// The default column order sorter.
        /// </summary>
        public static readonly DefaultColumnOrderSorter Default = new DefaultColumnOrderSorter();

        /// <summary>
        /// Sorts the columns in ascending alphabetical order.
        /// </summary>
        public static readonly AscendingColumnOrderSorter Ascending = new AscendingColumnOrderSorter();

        /// <summary>
        /// Sorts the columns in descending alphabetical order.
        /// </summary>
        public static readonly DescendingColumnOrderSorter Descending = new DescendingColumnOrderSorter();

        private readonly Func<IEnumerable<string>, IEnumerable<string>> _customSorter;

        /// <summary>
        /// Creates an object of type ColumnOrderSorter.
        /// </summary>
        /// <param name="customSorter">Custom sorting function. It takes the unsorted column names as an argument and returns the sorted column names.</param>
        public ColumnOrderSorter(Func<IEnumerable<string>, IEnumerable<string>> customSorter)
        {
            _customSorter = customSorter;
        }

        /// <summary>
        /// Called to sort the column names.
        /// </summary>
        /// <param name="columnNames">The column names to sort.</param>
        /// <returns>The sorted column names.</returns>
        public IEnumerable<string> Sort(IEnumerable<string> columnNames)
        {
            return _customSorter(columnNames);
        }
    }

    /// <summary>
    /// The default column order sorter.
    /// </summary>
    public class DefaultColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames) => columnNames;
    }

    /// <summary>
    /// Sorts the columns in ascending alphabetical order.
    /// </summary>
    public class AscendingColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames) => columnNames.OrderBy(c => c);
    }

    /// <summary>
    /// Sorts the columns in descending alphabetical order.
    /// </summary>
    public class DescendingColumnOrderSorter : IColumnOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> columnNames) => columnNames.OrderByDescending(c => c);
    }
}
