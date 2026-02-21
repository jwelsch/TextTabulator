using TextTabulator.Adapters.Generics;
using TextTabulator.Testing;

namespace TextTabulator.Adapters.GenericsTests
{
    public class DictionaryTabulatorAdapterTests
    {
        [Fact]
        public void When_dictionary_is_populated_with_class_values_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestClass7>()
            {
                ["One"] = new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                ["Two"] = new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                ["Three"] = new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new DictionaryTabulatorAdapter<string, TestClass7>(data, new DictionaryTabulatorAdapterOptions());

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Key", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty), i),
                i => Assert.Equal("_intField", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("One", j),
                    j => Assert.Equal("FirstProperty", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Two", j),
                    j => Assert.Equal("SecondProperty", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Three", j),
                    j => Assert.Equal("ThirdProperty", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_dictionary_is_populated_with_int_values_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, int>()
            {
                ["One"] = 1,
                ["Two"] = 2,
                ["Three"] = 3
            };

            var sut = new DictionaryTabulatorAdapter<string, int>(data, new DictionaryTabulatorAdapterOptions());
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Key", i),
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("One", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Two", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Three", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_dictionary_is_populated_with_string_values_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, string>()
            {
                ["One"] = "First",
                ["Two"] = "Second",
                ["Three"] = "Third"
            };

            var sut = new DictionaryTabulatorAdapter<string, string>(data, new DictionaryTabulatorAdapterOptions());
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Key", i),
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("One", j),
                    j => Assert.Equal("First", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Two", j),
                    j => Assert.Equal("Second", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Three", j),
                    j => Assert.Equal("Third", j)
                })
            });
        }
    }
}
