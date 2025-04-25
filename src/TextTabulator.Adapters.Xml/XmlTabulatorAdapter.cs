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
        private readonly IValueNormalizer _valueNormalizer = new ValueNormalizer();

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

            // Ensure that the stream is reset to the beginning.
            var stream = _xmlStreamProvider.Invoke();

            stream.Seek(0, SeekOrigin.Begin);

            using var xmlReader = XmlReader.Create(stream, _options.XmlReaderSettings);

            // Read until the first element is encountered. This should be the root node that contains the list
            // of XML nodes that will go in the table.
            if (!xmlReader.ReadToNode(XmlNodeType.Element))
            {
                throw new InvalidOperationException($"No root node found in XML document.");
            }

            var transformedHeaders = new List<string>();

            // Read first node in the list.
            if (xmlReader.ReadToNode(XmlNodeType.Element))
            {
                int? depth = null;
                var name = xmlReader.Name;

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && (depth ??= xmlReader.Depth) == xmlReader.Depth)
                    {
                        // Add the first occurances at this depth of the names of the XML nodes as the table headers.
                        var rawHeader = xmlReader.Name;
                        var transformed = _options.NodeNameTransform.Apply(rawHeader);
                        transformedHeaders.Add(transformed);
                        _headers.Add(rawHeader, new TableHeader(transformed, transformedHeaders.Count - 1));
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Depth == depth - 1 && xmlReader.Name == name)
                    {
                        // Once the end of the containing list node has been read, do not read any more nodes.
                        break;
                    }
                }
            }

            return transformedHeaders;
        }

        /// <summary>
        /// Called to return the row values. The outer enumeration is the rows, while the inner enumeration contains the values in each row.
        /// Can be an empty enumeration if the data contains no rows.
        /// </summary>
        /// <returns>An enumerable containing the rows and the values within each row.</returns>
        public IEnumerable<IEnumerable<string>> GetValueStrings()
        {
            // Ensure that the stream is reset to the beginning.
            var stream = _xmlStreamProvider.Invoke();

            stream.Seek(0, SeekOrigin.Begin);

            using var xmlReader = XmlReader.Create(stream, _options.XmlReaderSettings);

            // Read until the first element is encountered. This should be the root node that contains the list
            // of XML nodes that will go in the table.
            if (!xmlReader.ReadToNode(XmlNodeType.Element))
            {
                throw new InvalidOperationException($"No root node found in XML document.");
            }

            var values = new List<string[]>();
            var listDepth = xmlReader.Depth;
            var listName = xmlReader.Name;
            var itemDepth = listDepth + 1;
            string? itemName = null;
            var valueDepth = itemDepth + 1;
            string? valueName = null;
            var addedValue = false;
            string[]? rowValues = null;
            TableHeader? tableHeader = null;

            while (xmlReader.Read(XmlSkip.Whitespace | XmlSkip.Comment))
            {
                if (xmlReader.Name == listName && xmlReader.Depth == listDepth && xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    // The end of the list element has been reached.
                    break;
                }

                if (xmlReader.Depth == itemDepth)
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        // New XML list item.
                        itemName ??= xmlReader.Name;

                        rowValues = new string[_headers.Count];
                        Array.Fill(rowValues, "");
                        continue;
                    }

                    if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == itemName)
                    {
                        if (rowValues != null)
                        {
                            values.Add(rowValues);
                            rowValues = null;
                        }

                        // Reached the end element of the XML list item.
                        continue;
                    }
                }

                if (xmlReader.Depth == valueDepth)
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (!_headers.TryGetValue(xmlReader.Name, out tableHeader))
                        {
                            throw new InvalidOperationException($"Unknown header '{xmlReader.Name}' encountered while parsing values.");
                        }

                        if (xmlReader.IsEmptyElement && rowValues != null)
                        {
                            rowValues[tableHeader.Index] = string.Empty;
                            continue;
                        }

                        // New value in the list item. This should be a header.
                        valueName ??= xmlReader.Name;
                        continue;
                    }

                    if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == valueName)
                    {
                        // Reached the end element of the value item.
                        valueName = null;
                        addedValue = false;
                        continue;
                    }
                }

                if (xmlReader.Depth == valueDepth + 1 && !addedValue)
                {
                    if (xmlReader.NodeType == XmlNodeType.Text && tableHeader != null && rowValues != null)
                    {
                        rowValues[tableHeader.Index] = _valueNormalizer.Normalize(xmlReader.Value);
                        addedValue = true;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && tableHeader != null && rowValues != null)
                    {
                        rowValues[tableHeader.Index] = "<XML Nodes>";
                        addedValue = true;
                    }
                }
            }

            return values;
        }
    }
}
