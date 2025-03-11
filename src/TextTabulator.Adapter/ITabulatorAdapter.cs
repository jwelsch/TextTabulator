using System.Collections.Generic;

namespace TextTabulator.Adapter
{
    /// <summary>
    /// Interface for adapting different kinds of data to the format that the method Tabulator.Tabulate accepts.
    /// </summary>
    public interface ITabulatorAdapter
    {
        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings that will appear from left to right in the table, or null if the data contains no header strings.</returns>
        IEnumerable<string>? GetHeaderStrings();

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        IEnumerable<IEnumerable<string>> GetValueStrings();
    }
}
