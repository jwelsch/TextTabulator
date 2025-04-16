using System.Text;
using TextTabulator.Adapters.Xml;

namespace TextTabulator.Adapters.XmlTests
{
    public class XmlTabulatorAdapterTests
    {
        #region XML Data

        private readonly static string XmlWithSingleSimpleObject =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithMultipleSimpleObjects =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
        <test />
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
        <test />
    </dinosaur>
    <dinosaur>
        <name>Archaeopteryx</name>
        <weight>0.001</weight>
        <diet>Omnivore</diet>
        <extinction>147</extinction>
        <test />
    </dinosaur>
</dinosaurs>
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
        <test />
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithMultipleComplexObjects =
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
        <test />
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
        <formations>
            <formation>Evanston</formation>
            <formation>Scollard</formation>
            <formation>Laramie</formation>
            <formation>Lance</formation>
            <formation>Denver</formation>
            <formation>Hell Creek</formation>
        </formations>
        <bipedal>true</bipedal>
        <teeth>
            <shape>battery</shape>
            <length>4.5</length>
            <serrated>false</serrated>
            <count>800</count>
        </teeth>
        <test />
    </dinosaur>
    <dinosaur>
        <name>Archaeopteryx</name>
        <weight>0.001</weight>
        <diet>Omnivore</diet>
        <extinction>147</extinction>
        <formations>
            <formation>Solnhofen Limestone</formation>
        </formations>
        <bipedal>true</bipedal>
        <teeth>
            <shape>conical</shape>
            <length>0.1</length>
            <serrated>false</serrated>
            <count>8</count>
        </teeth>
        <test />
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithObjectsWithAnExtraProperty =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
        <extinction>66</extinction>
        <bipedal>false</bipedal>
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithObjectsMissingAProperty =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <name>Triceratops</name>
        <weight>8</weight>
        <diet>Herbivore</diet>
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithObjectsWithOutOfOrderProperties =
"""
<?xml version="1.0" encoding="UTF-8"?>
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
    <dinosaur>
        <diet>Herbivore</diet>
        <name>Triceratops</name>
        <extinction>66</extinction>
        <weight>8</weight>
    </dinosaur>
</dinosaurs>
""";

        private readonly static string XmlWithoutXmlDeclaration =
"""
<dinosaurs>
    <dinosaur>
        <name>Tyrannosaurus Rex</name>
        <weight>6.7</weight>
        <diet>Carnivore</diet>
        <extinction>66</extinction>
    </dinosaur>
</dinosaurs>
""";

        #endregion

        [Fact]
        public void When_xml_has_no_xml_declaration_then_headers_returned()
        {
            var sut = new XmlTabulatorAdapter(XmlWithoutXmlDeclaration);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i)
            );
        }

        [Fact]
        public void When_xml_is_single_simple_object_then_headers_returned()
        {
            var sut = new XmlTabulatorAdapter(XmlWithSingleSimpleObject);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i)
            );
        }

        [Fact]
        public void When_xml_is_single_simple_object_then_values_returned()
        {
            var sut = new XmlTabulatorAdapter(XmlWithSingleSimpleObject);

            _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(values);
            Assert.Collection(values, i =>
            {
                Assert.NotNull(i);
                Assert.Collection(i,
                    j => Assert.Equal("Tyrannosaurus Rex", j),
                    j => Assert.Equal("6.7", j),
                    j => Assert.Equal("Carnivore", j),
                    j => Assert.Equal("66", j)
                );
            });
        }

        //[Fact]
        //public void When_xml_is_single_complex_object_then_headers_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithSingleComplexObject);

        //    var headers = sut.GetHeaderStrings();

        //    Assert.NotNull(headers);
        //    Assert.Collection(headers,
        //        i => Assert.Equal("name", i),
        //        i => Assert.Equal("weight", i),
        //        i => Assert.Equal("diet", i),
        //        i => Assert.Equal("extinction", i),
        //        i => Assert.Equal("formations", i),
        //        i => Assert.Equal("bipedal", i),
        //        i => Assert.Equal("teeth", i),
        //        i => Assert.Equal("test", i)
        //    );
        //}

        //[Fact]
        //public void When_xml_is_single_complex_object_then_values_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithSingleComplexObject);

        //    _ = sut.GetHeaderStrings();
        //    var values = sut.GetValueStrings();

        //    Assert.NotNull(values);
        //    Assert.Collection(values, i =>
        //    {
        //        Assert.NotNull(i);
        //        Assert.Collection(i,
        //            j => Assert.Equal("Tyrannosaurus Rex", j),
        //            j => Assert.Equal("6.7", j),
        //            j => Assert.Equal("Carnivore", j),
        //            j => Assert.Equal("66", j),
        //            j => Assert.Equal("<JSON Array>", j),
        //            j => Assert.Equal("True", j),
        //            j => Assert.Equal("<JSON Object>", j),
        //            j => Assert.Equal("", j)
        //        );
        //    });
        //}

        //[Fact]
        //public void When_xml_is_multiple_simple_objects_then_headers_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithMultipleSimpleObjects);

        //    var headers = sut.GetHeaderStrings();

        //    Assert.NotNull(headers);
        //    Assert.Collection(headers,
        //        i => Assert.Equal("name", i),
        //        i => Assert.Equal("weight", i),
        //        i => Assert.Equal("diet", i),
        //        i => Assert.Equal("extinction", i)
        //    );
        //}

        //[Fact]
        //public void When_xml_is_multiple_simple_objects_then_values_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithMultipleSimpleObjects);

        //    _ = sut.GetHeaderStrings();
        //    var values = sut.GetValueStrings();

        //    Assert.NotNull(values);
        //    Assert.Collection(values,
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Tyrannosaurus Rex", j),
        //                j => Assert.Equal("6.7", j),
        //                j => Assert.Equal("Carnivore", j),
        //                j => Assert.Equal("66", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Triceratops", j),
        //                j => Assert.Equal("8", j),
        //                j => Assert.Equal("Herbivore", j),
        //                j => Assert.Equal("66", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Archaeopteryx", j),
        //                j => Assert.Equal("0.001", j),
        //                j => Assert.Equal("Omnivore", j),
        //                j => Assert.Equal("147", j)
        //            );
        //        });
        //}

        //[Fact]
        //public void When_xml_is_multiple_complex_objects_then_values_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithMultipleComplexObjects);

        //    _ = sut.GetHeaderStrings();
        //    var values = sut.GetValueStrings();

        //    Assert.NotNull(values);
        //    Assert.Collection(values,
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Tyrannosaurus Rex", j),
        //                j => Assert.Equal("6.7", j),
        //                j => Assert.Equal("Carnivore", j),
        //                j => Assert.Equal("66", j),
        //                j => Assert.Equal("<JSON Array>", j),
        //                j => Assert.Equal("True", j),
        //                j => Assert.Equal("<JSON Object>", j),
        //                j => Assert.Equal("", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Triceratops", j),
        //                j => Assert.Equal("8", j),
        //                j => Assert.Equal("Herbivore", j),
        //                j => Assert.Equal("66", j),
        //                j => Assert.Equal("<JSON Array>", j),
        //                j => Assert.Equal("False", j),
        //                j => Assert.Equal("<JSON Object>", j),
        //                j => Assert.Equal("", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Archaeopteryx", j),
        //                j => Assert.Equal("0.001", j),
        //                j => Assert.Equal("Omnivore", j),
        //                j => Assert.Equal("147", j),
        //                j => Assert.Equal("<JSON Array>", j),
        //                j => Assert.Equal("True", j),
        //                j => Assert.Equal("<JSON Object>", j),
        //                j => Assert.Equal("", j)
        //            );
        //        });
        //}

        //[Fact]
        //public void When_xml_is_objects_with_an_extra_property_then_throw()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithObjectsWithAnExtraProperty);

        //    _ = sut.GetHeaderStrings();

        //    Action action = () => sut.GetValueStrings();

        //    Assert.Throws<InvalidOperationException>(action);
        //}

        //[Fact]
        //public void When_xml_is_objects_missing_a_property_then_values_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithObjectsMissingAProperty);

        //    _ = sut.GetHeaderStrings();
        //    var values = sut.GetValueStrings();

        //    Assert.NotNull(values);
        //    Assert.Collection(values,
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Tyrannosaurus Rex", j),
        //                j => Assert.Equal("6.7", j),
        //                j => Assert.Equal("Carnivore", j),
        //                j => Assert.Equal("66", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Triceratops", j),
        //                j => Assert.Equal("8", j),
        //                j => Assert.Equal("Herbivore", j),
        //                j => Assert.Equal("", j)
        //            );
        //        });
        //}

        //[Fact]
        //public void When_xml_is_objects_with_out_of_order_properties_then_values_returned()
        //{
        //    var sut = new XmlTabulatorAdapter(XmlWithObjectsWithOutOfOrderProperties);

        //    _ = sut.GetHeaderStrings();
        //    var values = sut.GetValueStrings();

        //    Assert.NotNull(values);
        //    Assert.Collection(values,
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Tyrannosaurus Rex", j),
        //                j => Assert.Equal("6.7", j),
        //                j => Assert.Equal("Carnivore", j),
        //                j => Assert.Equal("66", j)
        //            );
        //        },
        //        i => {
        //            Assert.NotNull(i);
        //            Assert.Collection(i,
        //                j => Assert.Equal("Triceratops", j),
        //                j => Assert.Equal("8", j),
        //                j => Assert.Equal("Herbivore", j),
        //                j => Assert.Equal("66", j)
        //            );
        //        });
        //}

        //[Fact]
        //public void When_xml_is_from_streamprovider_then_headers_returned()
        //{
        //    var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWithSingleSimpleObject));
        //    Func<Stream> provider = () => stream;

        //    var sut = new XmlTabulatorAdapter(provider);

        //    var headers = sut.GetHeaderStrings();

        //    Assert.NotNull(headers);
        //    Assert.Collection(headers,
        //        i => Assert.Equal("name", i),
        //        i => Assert.Equal("weight", i),
        //        i => Assert.Equal("diet", i),
        //        i => Assert.Equal("extinction", i)
        //    );
        //}

        //[Fact]
        //public void When_xml_is_from_stream_then_headers_returned()
        //{
        //    var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(XmlWithSingleSimpleObject));
        //    var sut = new XmlTabulatorAdapter(stream);

        //    var headers = sut.GetHeaderStrings();

        //    Assert.NotNull(headers);
        //    Assert.Collection(headers,
        //        i => Assert.Equal("name", i),
        //        i => Assert.Equal("weight", i),
        //        i => Assert.Equal("diet", i),
        //        i => Assert.Equal("extinction", i)
        //    );
        //}
    }
}
