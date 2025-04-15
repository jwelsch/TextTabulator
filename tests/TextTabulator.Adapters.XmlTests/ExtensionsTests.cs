using System.Text;
using System.Xml;
using TextTabulator.Adapters.Xml;

namespace TextTabulator.Adapters.XmlTests
{
    public class ExtensionsTests
    {
        #region XML Data

        private readonly static string XmlNoWhitespaceNoComments =
"""
<?xml version="1.0" encoding="UTF-8"?><name>Foobar</name>
""";

        private readonly static string XmlWhitespaceNoComments =
"""
<?xml version="1.0" encoding="UTF-8"?>
<name>Foobar</name>
""";

        private readonly static string XmlNoWhitespaceComments =
"""
<?xml version="1.0" encoding="UTF-8"?><!-- Comment --><name>Foobar</name>
""";

        private readonly static string XmlWhitespaceComments =
"""
<?xml version="1.0" encoding="UTF-8"?>
<!-- Comment --><name>Foobar</name>
""";

        #endregion

        [Fact]
        public void When_xml_has_no_whitespace_no_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_no_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            reader.Read(XmlSkip.None);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_no_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_no_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            reader.Read(XmlSkip.Whitespace);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_no_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_no_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            reader.Read(XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_no_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_no_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_no_whitespace_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_xml_has_whitespace_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }
    }
}
