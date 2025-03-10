using TextTabulator.Adapter.Reflection;

namespace TextTabulator.Adapter.ReflectionTests
{
    internal class TestClass1
    {
    }

    internal class TestClass2
    {
        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();
    }

    internal class TestClass3
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly string _stringField;
        private readonly int _intField;
        private readonly IEnumerable<string> _enumerableField;
#pragma warning restore IDE0052 // Remove unread private members

        public TestClass3(string stringField, int intField, IEnumerable<string> enumerableField)
        {
            _stringField = stringField;
            _intField = intField;
            _enumerableField = enumerableField;
        }
    }

    internal class TestClass4
    {
        protected string StringProperty { get; set; } = string.Empty;

        protected int IntProperty { get; set; }

        protected IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();
    }

    internal class TestClass5
    {
        public readonly string _stringField = string.Empty;
#pragma warning disable CS0649
        public readonly int _intField;
#pragma warning restore CS0649
        public readonly IEnumerable<string> _enumerableField = Array.Empty<string>();
    }

    internal class TestClass6
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly string _stringField;
        private readonly int _intField;
        private readonly IEnumerable<string> _enumerableField;
#pragma warning restore IDE0052 // Remove unread private members

        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestClass6(string stringField, int intField, IEnumerable<string> enumerableField)
        {
            _stringField = stringField;
            _intField = intField;
            _enumerableField = enumerableField;
        }
    }

    internal struct TestStruct
    {
    }

    internal record TestRecord
    {
    }

    public class ClassReflectionTabulatorAdapterTests
    {
        [Fact]
        public void When_called_with_empty_enumberable_with_properties_then_data_returned()
        {
            var items = new TestClass2[0];

            var sut = new ClassReflectionTabulatorAdapter<TestClass2>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass2.StringProperty), i),
                i => Assert.Equal(nameof(TestClass2.IntProperty), i),
                i => Assert.Equal(nameof(TestClass2.EnumerableProperty), i),
            });

            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_public_access_and_with_multiple_objects_of_class_with_public_properties_then_data_returned()
        {
            var items = new TestClass2[]
            {
                new()
                {
                    StringProperty = "Hello",
                    IntProperty = 123,
                    EnumerableProperty = new string[] { "foo", "bar" },
                },
                new()
                {
                    StringProperty = "World",
                    IntProperty = 456,
                    EnumerableProperty = new string[] { "goo", "baz" },
                },
                new()
                {
                    StringProperty = "Hello World",
                    IntProperty = 789,
                    EnumerableProperty = new string[] { "noo", "buzz" },
                },
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass2>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass2.StringProperty), i),
                i => Assert.Equal(nameof(TestClass2.IntProperty), i),
                i => Assert.Equal(nameof(TestClass2.EnumerableProperty), i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[0].StringProperty, j),
                    j => Assert.Equal(items[0].IntProperty.ToString(), j),
                    j => Assert.Equal(items[0].EnumerableProperty.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[1].StringProperty, j),
                    j => Assert.Equal(items[1].IntProperty.ToString(), j),
                    j => Assert.Equal(items[1].EnumerableProperty.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[2].StringProperty, j),
                    j => Assert.Equal(items[2].IntProperty.ToString(), j),
                    j => Assert.Equal(items[2].EnumerableProperty.ToString(), j),
                }),
            });
        }

        [Fact]
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_class_with_public_properties_then_data_returned()
        {
            var items = new TestClass2[]
            {
                new()
                {
                    StringProperty = "Hello",
                    IntProperty = 123,
                    EnumerableProperty = new string[] { "foo", "bar" },
                },
                new()
                {
                    StringProperty = "World",
                    IntProperty = 456,
                    EnumerableProperty = new string[] { "goo", "baz" },
                },
                new()
                {
                    StringProperty = "Hello World",
                    IntProperty = 789,
                    EnumerableProperty = new string[] { "noo", "buzz" },
                },
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass2>(items, TypeMembers.Properties, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_object_of_class_with_no_properties_then_data_returned()
        {
            var items = new TestClass1[]
            {
                new(),
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass1>(items, TypeMembers.Properties, AccessModifiers.PublicNonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_class_with_private_fields_then_data_returned()
        {
            var items = new TestClass3[]
            {
                new( "Hello", 123, new string[] { "foo", "bar" }),
                new( "World", 456, new string[] { "goo", "baz" }),
                new( "Hello World", 789, new string[] { "noo", "buzz" }),
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass3>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            var stringFieldName = "_stringField";
            var intFieldName = "_intField";
            var enumerableFieldName = "_enumerableField";

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(stringFieldName, i),
                i => Assert.Equal(intFieldName, i),
                i => Assert.Equal(enumerableFieldName, i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], enumerableFieldName)?.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], enumerableFieldName)?.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], enumerableFieldName)?.ToString(), j),
                }),
            });
        }

        [Fact]
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_class_with_public_fields_then_data_returned()
        {
            var items = new TestClass5[]
            {
                new(),
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass5>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_multiple_objects_of_class_with_no_fields_then_data_returned()
        {
            var items = new TestClass1[]
            {
                new(),
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass1>(items, TypeMembers.Fields, AccessModifiers.PublicNonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_public_access_and_with_class_with_public_properties_and_private_field_then_return_data()
        {
            var items = new TestClass6[]
            {
                new("Hello", 123, new string[] { "foo", "bar" })
                {
                    StringProperty = "Hello",
                    IntProperty = 123,
                    EnumerableProperty = new string[] { "foo", "bar" },
                },
                new("World", 456, new string[] { "goo", "baz" })
                {
                    StringProperty = "World",
                    IntProperty = 456,
                    EnumerableProperty = new string[] { "goo", "baz" },
                },
                new("Hello World", 789, new string[] { "noo", "buzz" })
                {
                    StringProperty = "Hello World",
                    IntProperty = 789,
                    EnumerableProperty = new string[] { "noo", "buzz" },
                },
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass6>(items, TypeMembers.PropertiesFields);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass2.StringProperty), i),
                i => Assert.Equal(nameof(TestClass2.IntProperty), i),
                i => Assert.Equal(nameof(TestClass2.EnumerableProperty), i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[0].StringProperty, j),
                    j => Assert.Equal(items[0].IntProperty.ToString(), j),
                    j => Assert.Equal(items[0].EnumerableProperty.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[1].StringProperty, j),
                    j => Assert.Equal(items[1].IntProperty.ToString(), j),
                    j => Assert.Equal(items[1].EnumerableProperty.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[2].StringProperty, j),
                    j => Assert.Equal(items[2].IntProperty.ToString(), j),
                    j => Assert.Equal(items[2].EnumerableProperty.ToString(), j),
                }),
            });
        }

        [Fact]
        public void When_called_with_nonpublic_access_and_with_class_with_public_properties_and_private_field_then_return_data()
        {
            var items = new TestClass6[]
            {
                new("Hello", 123, new string[] { "foo", "bar" })
                {
                    StringProperty = "Hello",
                    IntProperty = 123,
                    EnumerableProperty = new string[] { "foo", "bar" },
                },
                new("World", 456, new string[] { "goo", "baz" })
                {
                    StringProperty = "World",
                    IntProperty = 456,
                    EnumerableProperty = new string[] { "goo", "baz" },
                },
                new("Hello World", 789, new string[] { "noo", "buzz" })
                {
                    StringProperty = "Hello World",
                    IntProperty = 789,
                    EnumerableProperty = new string[] { "noo", "buzz" },
                },
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass6>(items, TypeMembers.PropertiesFields, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            var stringFieldName = "_stringField";
            var intFieldName = "_intField";
            var enumerableFieldName = "_enumerableField";

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(stringFieldName, i),
                i => Assert.Equal(intFieldName, i),
                i => Assert.Equal(enumerableFieldName, i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[0], enumerableFieldName)?.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[1], enumerableFieldName)?.ToString(), j),
                }),
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], stringFieldName), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], intFieldName)?.ToString(), j),
                    j => Assert.Equal(PrivateMemberGetter.GetFieldValue(items[2], enumerableFieldName)?.ToString(), j),
                }),
            });
        }
    }
}
