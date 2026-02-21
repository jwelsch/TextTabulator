using TextTabulator.Adapters.Generics;
using TextTabulator.Testing;

namespace TextTabulator.Adapters.GenericsTests
{
    public class ListTabulatorAdapterTests
    {
        [Fact]
        public void When_list_is_empty_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>();

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(false));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass7.StringProperty), i),
                i => Assert.Equal("_intField", i),
            });
            Assert.Empty(values);
        }

        [Fact]
        public void When_list_is_empty_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>();

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(true));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty), i),
                i => Assert.Equal("_intField", i),
            });
            Assert.Empty(values);
        }

        [Fact]
        public void When_list_is_populated_with_class_values_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(true));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty), i),
                i => Assert.Equal("_intField", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("FirstProperty", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("SecondProperty", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("ThirdProperty", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_int_values_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<int>() { 1, 2, 3 };

            var sut = new ListTabulatorAdapter<int>(data, new ListTabulatorAdapterOptions(true));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("0", j), j => Assert.Equal("1", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("1", j), j => Assert.Equal("2", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("2", j), j => Assert.Equal("3", j) })
            });
        }

        [Fact]
        public void When_list_is_populated_with_string_values_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<string>() { "First", "Second", "Third" };

            var sut = new ListTabulatorAdapter<string>(data, new ListTabulatorAdapterOptions(true));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("0", j), j => Assert.Equal("First", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("1", j), j => Assert.Equal("Second", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("2", j), j => Assert.Equal("Third", j) })
            });
        }

        [Fact]
        public void When_list_is_populated_with_enum_values_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestEnum>() { TestEnum.First, TestEnum.Second, TestEnum.None };
            var sut = new ListTabulatorAdapter<TestEnum>(data, new ListTabulatorAdapterOptions(true));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();
            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
                i => Assert.Equal("Value", i),
            });
            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("0", j), j => Assert.Equal("First", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("1", j), j => Assert.Equal("Second", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("2", j), j => Assert.Equal("None", j) })
            });
        }

        [Fact]
        public void When_columns_are_sorted_in_ascending_order_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass9>()
            {
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new ListTabulatorAdapterOptions(true, null, SortOrder.AlphaNumericAscending);
            var sut = new ListTabulatorAdapter<TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
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
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
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
        public void When_columns_are_sorted_in_descending_order_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass9>()
            {
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new ListTabulatorAdapterOptions(true, null, SortOrder.AlphaNumericDescending);
            var sut = new ListTabulatorAdapter<TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Index", i),
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
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_class_and_header_name_transform_is_used_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(true, new UpperCaseNameTransform()));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("INDEX", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty).ToUpperInvariant(), i),
                i => Assert.Equal("_INTFIELD", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("FirstProperty", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("SecondProperty", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("ThirdProperty", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_int_and_header_name_transform_is_used_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<int>()
            {
                1,
                2,
                3
            };

            var sut = new ListTabulatorAdapter<int>(data, new ListTabulatorAdapterOptions(true, new UpperCaseNameTransform()));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("INDEX", i),
                i => Assert.Equal("VALUE", i)
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_header_name_transform_and_column_sort_order_is_used_and_includeIndex_is_true_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(true, new UpperCaseNameTransform(), SortOrder.AlphaNumericAscending));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("INDEX", i),
                i => Assert.Equal("_INTFIELD", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty).ToUpperInvariant(), i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("0", j),
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("FirstProperty", j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("SecondProperty", j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("3", j),
                    j => Assert.Equal("ThirdProperty", j),
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_class_values_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(false));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass7.StringProperty), i),
                i => Assert.Equal("_intField", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("FirstProperty", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("SecondProperty", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("ThirdProperty", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_int_values_then_headers_and_values_are_correct()
        {
            var data = new List<int>() { 1, 2, 3 };

            var sut = new ListTabulatorAdapter<int>(data, new ListTabulatorAdapterOptions(false));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("1", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("2", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("3", j) })
            });
        }

        [Fact]
        public void When_list_is_populated_with_string_values_then_headers_and_values_are_correct()
        {
            var data = new List<string>() { "First", "Second", "Third" };

            var sut = new ListTabulatorAdapter<string>(data, new ListTabulatorAdapterOptions(false));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Value", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("First", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("Second", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("Third", j) })
            });
        }

        [Fact]
        public void When_list_is_populated_with_enum_values_then_headers_and_values_are_correct()
        {
            var data = new List<TestEnum>() { TestEnum.First, TestEnum.Second, TestEnum.None };
            var sut = new ListTabulatorAdapter<TestEnum>(data, new ListTabulatorAdapterOptions(false));
            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();
            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("Value", i),
            });
            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("First", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("Second", j) }),
                i => Assert.Collection(i, new Action<string>[] { j => Assert.Equal("None", j) })
            });
        }

        [Fact]
        public void When_columns_are_sorted_in_ascending_order_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass9>()
            {
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new ListTabulatorAdapterOptions(false, null, SortOrder.AlphaNumericAscending);
            var sut = new ListTabulatorAdapter<TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
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
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("PrivateField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("PublicProperty", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
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
            var data = new List<TestClass9>()
            {
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" },
                new TestClass9("ProtectedField", "PrivateField", "ProtectedProperty", "PrivateProperty") { PublicProperty = "PublicProperty", PublicField = "PublicField" }
            };
            var options = new ListTabulatorAdapterOptions(false, null, SortOrder.AlphaNumericDescending);
            var sut = new ListTabulatorAdapter<TestClass9>(data, options);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);
            Assert.Collection(headers, new Action<string>[]
            {
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
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("PublicProperty", j),
                    j => Assert.Equal("PublicField", j),
                    j => Assert.Equal("ProtectedProperty", j),
                    j => Assert.Equal("ProtectedField", j),
                    j => Assert.Equal("PrivateProperty", j),
                    j => Assert.Equal("PrivateField", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_class_and_header_name_transform_is_used_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(false, new UpperCaseNameTransform()));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass7.StringProperty).ToUpperInvariant(), i),
                i => Assert.Equal("_INTFIELD", i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("FirstProperty", j),
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("SecondProperty", j),
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("ThirdProperty", j),
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_list_is_populated_with_int_and_header_name_transform_is_used_then_headers_and_values_are_correct()
        {
            var data = new List<int>()
            {
                1,
                2,
                3
            };

            var sut = new ListTabulatorAdapter<int>(data, new ListTabulatorAdapterOptions(false, new UpperCaseNameTransform()));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("VALUE", i)
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j)
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("3", j)
                })
            });
        }

        [Fact]
        public void When_header_name_transform_and_column_sort_order_is_used_then_headers_and_values_are_correct()
        {
            var data = new List<TestClass7>()
            {
                new TestClass7("FirstField", 1) { StringProperty = "FirstProperty", IntProperty = 11 },
                new TestClass7("SecondField", 2) { StringProperty = "SecondProperty", IntProperty = 22 },
                new TestClass7("ThirdField", 3) { StringProperty = "ThirdProperty", IntProperty = 33 }
            };

            var sut = new ListTabulatorAdapter<TestClass7>(data, new ListTabulatorAdapterOptions(false, new UpperCaseNameTransform(), SortOrder.AlphaNumericAscending));

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.NotNull(values);

            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal("_INTFIELD", i),
                i => Assert.Equal(nameof(TestClass7.StringProperty).ToUpperInvariant(), i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("1", j),
                    j => Assert.Equal("FirstProperty", j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("2", j),
                    j => Assert.Equal("SecondProperty", j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal("3", j),
                    j => Assert.Equal("ThirdProperty", j),
                })
            });
        }

        ////////////////////////////


    }
}
