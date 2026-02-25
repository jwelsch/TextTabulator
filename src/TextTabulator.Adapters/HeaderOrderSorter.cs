using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTabulator.Adapters
{
    /// <summary>
    /// Interface for sorting the headers themselves in a specific order.
    /// </summary>
    public interface IHeaderOrderSorter
    {
        /// <summary>
        /// Called to sort the header names.
        /// </summary>
        /// <param name="headerNames">The header names to sort.</param>
        /// <returns>The sorted header names.</returns>
        IEnumerable<string> Sort(IEnumerable<string> headerNames);
    }

    /// <summary>
    /// A header order sorter. Use the static members for built-in sorting options, or create a new instance with a custom sorting function.
    /// </summary>
    public class HeaderOrderSorter : IHeaderOrderSorter
    {
        /// <summary>
        /// The default header order sorter.
        /// </summary>
        public static readonly DefaultHeaderOrderSorter Default = new DefaultHeaderOrderSorter();

        /// <summary>
        /// Sorts the headers in ascending alphabetical order.
        /// </summary>
        public static readonly AscendingHeaderOrderSorter Ascending = new AscendingHeaderOrderSorter();

        /// <summary>
        /// Sorts the headers in descending alphabetical order.
        /// </summary>
        public static readonly DescendingHeaderOrderSorter Descending = new DescendingHeaderOrderSorter();

        private readonly Func<IEnumerable<string>, IEnumerable<string>> _customSorter;

        /// <summary>
        /// Creates an object of type HeaderOrderSorter.
        /// </summary>
        /// <param name="customSorter">Custom sorting function. It takes the unsorted header names as an argument and returns the sorted header names.</param>
        public HeaderOrderSorter(Func<IEnumerable<string>, IEnumerable<string>> customSorter)
        {
            _customSorter = customSorter;
        }

        /// <summary>
        /// Called to sort the header names.
        /// </summary>
        /// <param name="headerNames">The header names to sort.</param>
        /// <returns>The sorted header names.</returns>
        public IEnumerable<string> Sort(IEnumerable<string> headerNames)
        {
            return _customSorter(headerNames);
        }
    }

    /// <summary>
    /// The default header order sorter.
    /// </summary>
    public class DefaultHeaderOrderSorter : IHeaderOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> headerNames) => headerNames;
    }

    /// <summary>
    /// Sorts the headers in ascending alphabetical order.
    /// </summary>
    public class AscendingHeaderOrderSorter : IHeaderOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> headerNames) => headerNames.OrderBy(c => c);
    }

    /// <summary>
    /// Sorts the headers in descending alphabetical order.
    /// </summary>
    public class DescendingHeaderOrderSorter : IHeaderOrderSorter
    {
        public IEnumerable<string> Sort(IEnumerable<string> headerNames) => headerNames.OrderByDescending(c => c);
    }
}
