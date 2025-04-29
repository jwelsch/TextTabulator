using System.Text;
using TextTabulator.Adapters.YamlDotNet;
using YamlDotNet.Core;

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

        private readonly static string YamlWithObjectsNotInASequence =
"""
name: Tyrannosaurus Rex
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
""";

        #endregion

        [Fact]
        public void When_yaml_is_single_simple_object_then_headers_returned()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithSingleSimpleObject));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithSingleSimpleObject));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);
            
            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithSingleComplexObject));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithSingleComplexObject));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithMultipleSimpleObjects));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithMultipleSimpleObjects));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithMultipleComplexObjects));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithObjectsWithAnExtraProperty));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);
            
            var sut = new YamlDotNetTabulatorAdapter(parser);

            _ = sut.GetHeaderStrings();

            Action action = () => sut.GetValueStrings();

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void When_yaml_is_objects_missing_a_property_then_values_returned()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithObjectsMissingAProperty));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithObjectsWithOutOfOrderProperties));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

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
        public void When_yaml_is_object_not_in_a_sequence_then_invalidoperationexception_thrown()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(YamlWithObjectsNotInASequence));
            using var reader = new StreamReader(stream);
            var parser = new Parser(reader);

            var sut = new YamlDotNetTabulatorAdapter(parser);

            Assert.Throws<InvalidOperationException>(() => sut.GetHeaderStrings());
        }
    }
}
