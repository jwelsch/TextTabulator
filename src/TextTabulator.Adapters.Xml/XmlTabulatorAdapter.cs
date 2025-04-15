using System.Collections.Generic;

namespace TextTabulator.Adapters.Xml
{
    /// <summary>
    /// Public interface for IXmlTabulatorAdapter.
    /// </summary>
    public interface IXmlTabulatorAdapter : ITabulatorAdapter
    {
    }

    /// <summary>
    /// The adapter class that accepts XML data and presents the data that it reads in a format that TextTabulator.Tabulate can consume.
    /// 
    /// The data should be in the following format:
    ///
    /// <?xml version="1.0" encoding="UTF-8"?>
    /// <list>
    ///     <object>
    ///         <value1>value1A</value1>
    ///         <value2>value2A</value2>
    ///     </object>
    ///     <object>
    ///         <value1>value1B</value1>
    ///         <value2>value2B</value2>
    ///     </object>
    /// <object>
    ///         <value1>value1C</value1>
    ///         <value2>value2C</value2>
    ///     </object>
    ///     ...
    /// </list>
    /// 
    /// </summary>
    public class XmlTabulatorAdapter : IXmlTabulatorAdapter
    {
        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            throw new System.NotImplementedException();
        }
    }
}
