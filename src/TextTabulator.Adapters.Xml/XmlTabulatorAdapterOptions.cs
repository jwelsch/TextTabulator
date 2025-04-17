using System.Xml;

namespace TextTabulator.Adapters.Xml
{
    /// <summary>
    /// Options to allow configuration of the XmlTabulatorAdapter class.
    /// </summary>
    public class XmlTabulatorAdapterOptions
    {
        /// <summary>
        /// Gets the transform to apply to XML node names.
        /// </summary>
        public INameTransform NodeNameTransform { get; }

        /// <summary>
        /// Gets settings that specify a set of features to support on the XmlReader object created by the Create method.
        /// </summary>
        public XmlReaderSettings XmlReaderSettings { get; }

        /// <summary>
        /// Creates an object of type XmlTabulatorAdapterOptions.
        /// </summary>
        /// <param name="nodeNameTransform">Transform to apply to XML node names. Passing null will cause the XML node names to not be altered.</param>
        /// <param name="xmlReaderSettings">Specifies a set of features to support on the XmlReader object created by the Create method. Passing null will use an XmlReaderSettings object with default values.</param>
        public XmlTabulatorAdapterOptions(INameTransform? nodeNameTransform = null, XmlReaderSettings? xmlReaderSettings = null)
        {
            NodeNameTransform = nodeNameTransform ?? new PassThruNameTransform();
            XmlReaderSettings = xmlReaderSettings ?? new XmlReaderSettings();
        }
    }
}
