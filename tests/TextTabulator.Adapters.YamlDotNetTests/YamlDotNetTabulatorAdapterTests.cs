using System.Text;
using TextTabulator.Adapters.YamlDotNet;
using YamlDotNet.Serialization;

namespace TextTabulator.Adapters.YamlDotNetTests
{
    public class YamlDotNetTabulatorAdapterTests
    {
        #region YAML Data

        private readonly static string YamlWithSingleSimpleObject =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
  test:
""";

        private readonly static string YamlWithMultipleSimpleObjects =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
  test: 
- name: Triceratops
  weight: 8
  diet: Herbivore
  extinction: 66
  test: null
- name: Archaeopteryx
  weight: 0.001
  diet: Omnivore
  extinction: 147
  test: null
""";

        private readonly static string YamlWithSingleComplexObject =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
  formations:
    - Hell Creek
    - Lance
    - North Horn
    - Javelina
  bipedal: true
  teeth:
    shape: conical
    length: 30
    serrated: true
    count: 60
  test: null
""";

        private readonly static string YamlWithMultipleComplexObjects =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
  formations:
    - Hell Creek
    - Lance
    - North Horn
    - Javelina
  bipedal: true
  teeth:
    shape: conical
    length: 30
    serrated: true
    count: 60
  test: null
- name: Triceratops
  weight: 8
  diet: Herbivore
  extinction: 66
  formations:
    - Evanston
    - Scollard
    - Laramie
    - Lance
    - Denver
    - Hell Creek
  bipedal: false
  teeth:
    shape: battery
    length: 4.5
    serrated: false
    count: 800
  test: null
- name: Archaeopteryx
  weight: 0.001
  diet: Omnivore
  extinction: 147
  formations:
    - Solnhofen Limestone
  bipedal: true
  teeth:
    shape: conical
    length: 0.1
    serrated: false
    count: 8
  test: null
""";

        private readonly static string YamlWithObjectsWithAnExtraProperty =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
- name: Triceratops
  weight: 8
  diet: Herbivore
  extinction: 66
  bipedal: false
""";

        private readonly static string YamlWithObjectsMissingAProperty =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
- name: Triceratops
  weight: 8
  diet: Herbivore
""";

        private readonly static string YamlWithObjectsWithOutOfOrderProperties =
"""
- name: Tyrannosaurus Rex
  weight: 6.7
  diet: Carnivore
  extinction: 66
- diet: Herbivore
  name: Triceratops
  extinction: 66
  weight: 8
""";

        #endregion

        [Fact]
        public void When_yaml_is_single_simple_object_then_headers_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithSingleSimpleObject);
            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i),
                i => Assert.Equal("test", i)
            );
        }

        [Fact]
        public void When_yaml_is_single_simple_object_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithSingleSimpleObject);

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
                    j => Assert.Equal("66", j),
                    j => Assert.Equal("", j)
                );
            });
        }

        [Fact]
        public void When_yaml_is_single_complex_object_then_headers_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithSingleComplexObject);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i),
                i => Assert.Equal("formations", i),
                i => Assert.Equal("bipedal", i),
                i => Assert.Equal("teeth", i),
                i => Assert.Equal("test", i)
            );
        }

        [Fact]
        public void When_yaml_is_single_complex_object_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithSingleComplexObject);

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
                    j => Assert.Equal("66", j),
                    j => Assert.Equal("<YAML Array>", j),
                    j => Assert.Equal("True", j),
                    j => Assert.Equal("<YAML Object>", j),
                    j => Assert.Equal("", j)
                );
            });
        }

        [Fact]
        public void When_yaml_is_multiple_simple_objects_then_headers_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithMultipleSimpleObjects);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i),
                i => Assert.Equal("test", i)
            );
        }

        [Fact]
        public void When_yaml_is_multiple_simple_objects_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithMultipleSimpleObjects);

            _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(values);
            Assert.Collection(values,
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Tyrannosaurus Rex", j),
                        j => Assert.Equal("6.7", j),
                        j => Assert.Equal("Carnivore", j),
                        j => Assert.Equal("66", j),
                        j => Assert.Equal("", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Triceratops", j),
                        j => Assert.Equal("8", j),
                        j => Assert.Equal("Herbivore", j),
                        j => Assert.Equal("66", j),
                        j => Assert.Equal("", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Archaeopteryx", j),
                        j => Assert.Equal("0.001", j),
                        j => Assert.Equal("Omnivore", j),
                        j => Assert.Equal("147", j),
                        j => Assert.Equal("", j)
                    );
                });
        }

        [Fact]
        public void When_yaml_is_multiple_complex_objects_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithMultipleComplexObjects);

            _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(values);
            Assert.Collection(values,
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Tyrannosaurus Rex", j),
                        j => Assert.Equal("6.7", j),
                        j => Assert.Equal("Carnivore", j),
                        j => Assert.Equal("66", j),
                        j => Assert.Equal("<YAML Array>", j),
                        j => Assert.Equal("True", j),
                        j => Assert.Equal("<YAML Object>", j),
                        j => Assert.Equal("", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Triceratops", j),
                        j => Assert.Equal("8", j),
                        j => Assert.Equal("Herbivore", j),
                        j => Assert.Equal("66", j),
                        j => Assert.Equal("<YAML Array>", j),
                        j => Assert.Equal("False", j),
                        j => Assert.Equal("<YAML Object>", j),
                        j => Assert.Equal("", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Archaeopteryx", j),
                        j => Assert.Equal("0.001", j),
                        j => Assert.Equal("Omnivore", j),
                        j => Assert.Equal("147", j),
                        j => Assert.Equal("<YAML Array>", j),
                        j => Assert.Equal("True", j),
                        j => Assert.Equal("<YAML Object>", j),
                        j => Assert.Equal("", j)
                    );
                });
        }

        [Fact]
        public void When_yaml_is_objects_with_an_extra_property_then_throw()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithObjectsWithAnExtraProperty);

            _ = sut.GetHeaderStrings();

            Action action = () => sut.GetValueStrings();

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void When_yaml_is_objects_missing_a_property_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithObjectsMissingAProperty);

            _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(values);
            Assert.Collection(values,
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Tyrannosaurus Rex", j),
                        j => Assert.Equal("6.7", j),
                        j => Assert.Equal("Carnivore", j),
                        j => Assert.Equal("66", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Triceratops", j),
                        j => Assert.Equal("8", j),
                        j => Assert.Equal("Herbivore", j),
                        j => Assert.Equal("", j)
                    );
                });
        }

        [Fact]
        public void When_yaml_is_objects_with_out_of_order_properties_then_values_returned()
        {
            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, YamlWithObjectsWithOutOfOrderProperties);

            _ = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(values);
            Assert.Collection(values,
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Tyrannosaurus Rex", j),
                        j => Assert.Equal("6.7", j),
                        j => Assert.Equal("Carnivore", j),
                        j => Assert.Equal("66", j)
                    );
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Triceratops", j),
                        j => Assert.Equal("8", j),
                        j => Assert.Equal("Herbivore", j),
                        j => Assert.Equal("66", j)
                    );
                });
        }

        [Fact]
        public void When_yaml_is_from_stringprovider_then_headers_returned()
        {
            StringProvider provider = () => YamlWithSingleSimpleObject;

            var deserializer = new Deserializer();

            var sut = new YamlDotNetTabulatorAdapter(deserializer, provider);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i),
                i => Assert.Equal("test", i)
            );
        }

        [Fact]
        public void When_yaml_is_from_textreader_then_headers_returned()
        {
            using var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(YamlWithSingleSimpleObject));
            using var reader = new StreamReader(stream);
            var deserializer = new Deserializer();
            var sut = new YamlDotNetTabulatorAdapter(deserializer, reader);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("name", i),
                i => Assert.Equal("weight", i),
                i => Assert.Equal("diet", i),
                i => Assert.Equal("extinction", i),
                i => Assert.Equal("test", i)
            );
        }
    }
}
