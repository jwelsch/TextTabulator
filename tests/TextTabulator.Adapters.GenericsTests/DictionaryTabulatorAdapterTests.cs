using TextTabulator.Adapters.Generics;
using TextTabulator.Testing;

namespace TextTabulator.Adapters.GenericsTests
{
    public class DictionaryTabulatorAdapterTests
    {
        [Fact]
        public void When_dictionary_is_empty_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestClass7>();

            var sut = new DictionaryTabulatorAdapter<string, TestClass7>(data);

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
            Assert.Empty(values);
        }

        [Fact]
        public void When_dictionary_is_populated_with_class_values_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestClass7>()
            {
                ["One"] = new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                ["Two"] = new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                ["Three"] = new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new DictionaryTabulatorAdapter<string, TestClass7>(data);

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

            var sut = new DictionaryTabulatorAdapter<string, int>(data);
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

            var sut = new DictionaryTabulatorAdapter<string, string>(data);
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

        [Fact]
        public void When_dictionary_is_populated_with_enum_values_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestEnum>()
            {
                ["One"] = TestEnum.First,
                ["Two"] = TestEnum.Second,
                ["Three"] = TestEnum.None
            };
            var sut = new DictionaryTabulatorAdapter<string, TestEnum>(data);
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
                    j => Assert.Equal("None", j)
                })
            });
        }

        [Fact]
        public void When_columns_are_sorted_in_ascending_order_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestClass9>()
            {
                ["One"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                ["Two"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                ["Three"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new DictionaryTabulatorAdapterOptions(null, SortOrder.AlphaNumericAscending);
            var sut = new DictionaryTabulatorAdapter<string, TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Key", i),
                i => Assert.Equal("PrivateField", i),
                i => Assert.Equal("PrivateProperty", i),
                i => Assert.Equal("ProtectedField", i),
                i => Assert.Equal("ProtectedProperty", i),
                i => Assert.Equal("PublicField", i),
                i => Assert.Equal("PublicProperty", i)
            });
            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("One", j),
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Two", j),
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Three", j),
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                })
            });
        }

        [Fact]
        public void When_columns_are_sorted_in_descending_order_then_headers_and_values_are_correct()
        {
            var data = new Dictionary<string, TestClass9>()
            {
                ["One"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                ["Two"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                ["Three"] = new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new DictionaryTabulatorAdapterOptions(null, SortOrder.AlphaNumericDescending);
            var sut = new DictionaryTabulatorAdapter<string, TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Key", i),
                i => Assert.Equal("PublicProperty", i),
                i => Assert.Equal("PublicField", i),
                i => Assert.Equal("ProtectedProperty", i),
                i => Assert.Equal("ProtectedField", i),
                i => Assert.Equal("PrivateProperty", i),
                i => Assert.Equal("PrivateField", i)
            });
            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("One", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Two", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("Three", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                })
            });
        }
    }
}
