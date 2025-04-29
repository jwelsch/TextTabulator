using System.Text;
using TextTabulator.Adapters.Json;

namespace TextTabulator.Adapters.JsonTests
{
    public class JsonTabulatorAdapterTests
    {
        #region JSON Data

        private readonly static string JsonWithSingleSimpleObject =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
  }
]
""";

        private readonly static string JsonWithMultipleSimpleObjects =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
  },
  {
    "name": "Triceratops",
    "weight": 8,
    "diet": "Herbivore",
    "extinction": 66
  },
  {
    "name": "Archaeopteryx",
    "weight": 0.001,
    "diet": "Omnivore",
    "extinction": 147
  }
]
""";

        private readonly static string JsonWithSingleComplexObject =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66,
    "formations":
    [
        "Hell Creek",
        "Lance",
        "North Horn",
        "Javelina"
    ],
    "bipedal": true,
    "teeth":
    {
        "shape": "conical",
        "length": 30,
        "serrated": true,
        "count": 60
    },
    "test": null
  }
]
""";

        private readonly static string JsonWithMultipleComplexObjects =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66,
    "formations":
    [
      "Hell Creek",
      "Lance",
      "North Horn",
      "Javelina"
    ],
    "bipedal": true,
    "teeth":
    {
      "shape": "conical",
      "length": 30,
      "serrated": true,
      "count": 60
    },
    "test": null
  },
  {
    "name": "Triceratops",
    "weight": 8,
    "diet": "Herbivore",
    "extinction": 66,
    "formations":
    [
      "Evanston",
      "Scollard",
      "Laramie",
      "Lance",
      "Denver",
      "Hell Creek"
    ],
    "bipedal": false,
    "teeth":
    {
      "shape": "battery",
      "length": 4.5,
      "serrated": false,
      "count": 800
    },
    "test": null
  },
  {
    "name": "Archaeopteryx",
    "weight": 0.001,
    "diet": "Omnivore",
    "extinction": 147,
    "formations":
    [
      "Solnhofen Limestone"
    ],
    "bipedal": true,
    "teeth":
    {
      "shape": "conical",
      "length": 0.1,
      "serrated": false,
      "count": 8
    },
    "test": null
  }
]
""";

        private readonly static string JsonWithObjectsWithAnExtraProperty =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
  },
  {
    "name": "Triceratops",
    "weight": 8,
    "diet": "Herbivore",
    "extinction": 66,
    "bipedal": false
  }
]
""";

        private readonly static string JsonWithObjectsMissingAProperty =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
  },
  {
    "name": "Triceratops",
    "weight": 8,
    "diet": "Herbivore"
  }
]
""";

        private readonly static string JsonWithObjectsWithOutOfOrderProperties =
"""
[
  {
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
  },
  {
    "diet": "Herbivore",
    "name": "Triceratops",
    "extinction": 66,
    "weight": 8
  }
]
""";

        private readonly static string JsonWithObjectNotInAList =
"""
{
    "name": "Tyrannosaurus Rex",
    "weight": 6.7,
    "diet": "Carnivore",
    "extinction": 66
}
""";

        #endregion

        [Fact]
        public void When_json_is_single_simple_object_then_headers_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithSingleSimpleObject);

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
        public void When_json_is_single_simple_object_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithSingleSimpleObject);

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

        [Fact]
        public void When_json_is_single_complex_object_then_headers_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithSingleComplexObject);

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
        public void When_json_is_single_complex_object_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithSingleComplexObject);

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
                    j => Assert.Equal("<JSON Array>", j),
                    j => Assert.Equal("True", j),
                    j => Assert.Equal("<JSON Object>", j),
                    j => Assert.Equal("", j)
                );
            });
        }

        [Fact]
        public void When_json_is_multiple_simple_objects_then_headers_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithMultipleSimpleObjects);

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
        public void When_json_is_multiple_simple_objects_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithMultipleSimpleObjects);

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
                },
                i => {
                    Assert.NotNull(i);
                    Assert.Collection(i,
                        j => Assert.Equal("Archaeopteryx", j),
                        j => Assert.Equal("0.001", j),
                        j => Assert.Equal("Omnivore", j),
                        j => Assert.Equal("147", j)
                    );
                });
        }

        [Fact]
        public void When_json_is_multiple_complex_objects_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithMultipleComplexObjects);

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
                        j => Assert.Equal("<JSON Array>", j),
                        j => Assert.Equal("True", j),
                        j => Assert.Equal("<JSON Object>", j),
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
                        j => Assert.Equal("<JSON Array>", j),
                        j => Assert.Equal("False", j),
                        j => Assert.Equal("<JSON Object>", j),
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
                        j => Assert.Equal("<JSON Array>", j),
                        j => Assert.Equal("True", j),
                        j => Assert.Equal("<JSON Object>", j),
                        j => Assert.Equal("", j)
                    );
                });
        }

        [Fact]
        public void When_json_is_objects_with_an_extra_property_then_throw()
        {
            var sut = new JsonTabulatorAdapter(JsonWithObjectsWithAnExtraProperty);

            _ = sut.GetHeaderStrings();

            Action action = () => sut.GetValueStrings();

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void When_json_is_objects_missing_a_property_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithObjectsMissingAProperty);

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
        public void When_json_is_objects_with_out_of_order_properties_then_values_returned()
        {
            var sut = new JsonTabulatorAdapter(JsonWithObjectsWithOutOfOrderProperties);

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
        public void When_json_is_from_streamprovider_then_headers_returned()
        {
            using var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(JsonWithSingleSimpleObject));
            Func<Stream> provider = () => stream;

            var sut = new JsonTabulatorAdapter(provider);

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
        public void When_json_is_from_stream_then_headers_returned()
        {
            using var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(JsonWithSingleSimpleObject));
            var sut = new JsonTabulatorAdapter(stream);

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
        public void When_json_is_object_not_in_list_then_invalidoperationexception_thrown()
        {
            using var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(JsonWithObjectNotInAList));
            var sut = new JsonTabulatorAdapter(stream);

            Assert.Throws<InvalidOperationException>(() => sut.GetHeaderStrings());
        }

        [Fact]
        public void When_name_transform_used_then_transformed_headers_returned()
        {
            var transform = new CamelNameTransform();
            var options = new JsonTabulatorAdapterOptions(transform);

            var sut = new JsonTabulatorAdapter(JsonWithSingleSimpleObject, options);

            var headers = sut.GetHeaderStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers,
                i => Assert.Equal("Name", i),
                i => Assert.Equal("Weight", i),
                i => Assert.Equal("Diet", i),
                i => Assert.Equal("Extinction", i)
            );
        }
    }
}
