using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

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
        private readonly Func<Stream> _xmlStreamProvider;
        private readonly XmlTabulatorAdapterOptions _options;
        private readonly Dictionary<string, TableHeader> _headers = new Dictionary<string, TableHeader>();

        /// <summary>
        /// Creates an object of type XmlTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.
        /// </summary>
        /// <param name="xmlStreamProvider">Function that provides a stream containing a list containing homogeneous objects of UTF-8 encoded XML data.</param>
        /// <param name="options">Options for the adapter.</param>
        public XmlTabulatorAdapter(Func<Stream> xmlStreamProvider, XmlTabulatorAdapterOptions? options = null)
        {
            _xmlStreamProvider = xmlStreamProvider;
            _options = options ?? new XmlTabulatorAdapterOptions();
        }

        /// <summary>
        /// Creates an object of type XmlTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.
        /// </summary>
        /// <param name="xmlStream">Stream containing UTF-8 encoded XML data.</param>
        /// <param name="options">Options for the adapter.</param>
        public XmlTabulatorAdapter(Stream xmlStream, XmlTabulatorAdapterOptions? options = null)
            : this(() => xmlStream, options)
        {
        }

        /// <summary>
        /// Creates an object of type XmlTabulatorAdapter.
        /// In order for the adpater to function correctly, make sure the XML data only contains a list of homogeneous XML objects.
        /// </summary>
        /// <param name="xml">String containing raw XML data.</param>
        /// <param name="options">Options for the adapter.</param>
        public XmlTabulatorAdapter(string xml, XmlTabulatorAdapterOptions? options = null)
            : this(() => new MemoryStream(UTF8Encoding.UTF8.GetBytes(xml)), options)
        {
        }

        /// <summary>
        /// Called to return the header strings, if any, of the data. If the data does not contain headers, then null should be returned.
        /// </summary>
        /// <returns>An enumerable containing the header strings for the table, or null if the data contains no header strings.</returns>
        public IEnumerable<string>? GetHeaderStrings()
        {
            _headers.Clear();

            using var xmlReader = XmlReader.Create(_xmlStreamProvider.Invoke(), _options.XmlReaderSettings);

            // Read until the first element is encountered. This should be the root node that contains the list
            // of XML nodes that will go in the table.
            if (!xmlReader.ReadToNode(XmlNodeType.Element))
            {
                throw new InvalidOperationException($"No root node found in XML document.");
            }

            var headers = new List<string>();

            // Read first node in the list.
            if (xmlReader.ReadToNode(XmlNodeType.Element))
            {
                var innerDepth = 0;

                // Read node names within the XML node that will go in the table.
                while (xmlReader.ReadToNode(XmlNodeType.Element, x => innerDepth == 0 || xmlReader.Depth == innerDepth))
                {
                    if (innerDepth == 0)
                    {
                        innerDepth = xmlReader.Depth;
                    }

                    headers.Add(xmlReader.Name);
                }
            }

            return headers;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            using var xmlReader = XmlReader.Create(_xmlStreamProvider.Invoke(), _options.XmlReaderSettings);

            // Read until the first element is encountered. This should be the root node that contains the list
            // of XML nodes that will go in the table.
            if (!xmlReader.ReadToNode(XmlNodeType.Element))
            {
                throw new InvalidOperationException($"No root node found in XML document.");
            }

            var values = new List<List<string>>();
            int? listDepth = null;

            // Read all nodes in the list.
            while (xmlReader.ReadToNode(XmlNodeType.Element, x => (listDepth ??= xmlReader.Depth) == xmlReader.Depth))
            {
                int? innerDepth = null;
                var rowValues = new List<string>();

                // Read nodes within the XML node that will go in the table.
                // This only reads the nodes that are direct children of the list node.
                while (xmlReader.ReadToNode(XmlNodeType.Element, x => (innerDepth ??= xmlReader.Depth) == xmlReader.Depth))
                {
                    var content = xmlReader.ReadElementContentAsString();
                    rowValues.Add(content);
                }

                values.Add(rowValues);
            }

            return values;
        }
    }
}
