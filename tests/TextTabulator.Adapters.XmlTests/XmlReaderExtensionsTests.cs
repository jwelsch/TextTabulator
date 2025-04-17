using System.Text;
using System.Xml;
using TextTabulator.Adapters.Xml;

namespace TextTabulator.Adapters.XmlTests
{
    public class XmlReaderExtensionsTests
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

        private readonly static string XmlWithSingleComplexObject =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
        <formations>
            <formation>Hell Creek</formation>
            <formation>Lance</formation>
            <formation>North Horn</formation>
            <formation>Javelina</formation>
        </formations>
        <bipedal>true</bipedal>
        <teeth>
            <shape>conical</shape>
            <length>30</length>
            <serrated>true</serrated>
            <count>60</count>
        </teeth>
    </dinosaur>
</dinosaurs>
""";

        #endregion

        [Fact]
        public void When_read_with_xml_has_no_whitespace_no_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_no_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            var result = reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_comments_and_no_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            var result = reader.Read(XmlSkip.None);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            result = reader.Read(XmlSkip.None);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_no_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_no_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            var result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_comments_and_skip_whitespace_no_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            var result = reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.Equal(XmlNodeType.Comment, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_no_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_no_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            var result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_comments_and_no_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            var result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.Whitespace, reader.NodeType);

            result = reader.Read(XmlSkip.Comment);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_no_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_no_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            var result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_no_whitespace_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlNoWhitespaceComments)));

            var result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_read_with_xml_has_whitespace_comments_and_skip_whitespace_skip_comments_nothing_skipped()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            var result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal(XmlNodeType.XmlDeclaration, reader.NodeType);

            result = reader.Read(XmlSkip.Whitespace | XmlSkip.Comment);
            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_readtonode_specifies_node_type_then_correct_node_returned()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceComments)));

            var result = reader.ReadToNode(XmlNodeType.Element);

            Assert.True(result);
            Assert.Equal("name", reader.Name);
        }

        [Fact]
        public void When_readtonode_specifies_node_type_and_test_then_correct_node_returned()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWithSingleComplexObject)));

            var count = 0;
            var result = reader.ReadToNode(XmlNodeType.Element, x =>
            {
                if (x.Name == "formation")
                {
                    if (count == 1)
                    {
                        return true;
                    }

                    count++;
                }

                return false;
            });

            Assert.True(result);
            Assert.Equal("Lance", reader.ReadElementContentAsString());
        }

        [Fact]
        public void When_readtonode_specifies_nonexisting_node_type_then_no_node_returned()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWhitespaceNoComments)));

            var result = reader.ReadToNode(XmlNodeType.Comment);

            Assert.False(result);
        }

        [Fact]
        public void When_readtonode_specifies_node_type_and_test_that_never_returns_true_then_no_node_returned()
        {
            using var reader = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWithSingleComplexObject)));

            var result = reader.ReadToNode(XmlNodeType.Element, x => false);

            Assert.False(result);
        }
    }
}
