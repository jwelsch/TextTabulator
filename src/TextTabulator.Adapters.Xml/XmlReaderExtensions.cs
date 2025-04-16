using System;
using System.Xml;

namespace TextTabulator.Adapters.Xml
{
    [Flags]
    internal enum XmlSkip : byte
    {
        None = 0x00,
        Whitespace = 0x01,
        Comment = 0x10
    }

    internal static class XmlReaderExtensions
    {
        internal static bool Read(this XmlReader xmlReader, XmlSkip skip)
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Whitespace)
                {
                    if ((skip & XmlSkip.Whitespace) == 0)
                    {
                        return true;
                    }
                }
                else if (xmlReader.NodeType == XmlNodeType.Comment)
                {
                    if ((skip & XmlSkip.Comment) == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool ReadToNode(this XmlReader xmlReader, XmlNodeType nodeType, Func<XmlReader, bool>? test = null)
        {
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == nodeType
                    && (test == null
                        || test.Invoke(xmlReader)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
