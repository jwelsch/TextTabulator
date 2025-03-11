using TextTabulator.Adapter.Reflection;

namespace TextTabulator.Adapter.ReflectionTests
{
    public class ReflectionTabulatorAdapterTests
    {
        [Fact]
        public void When_called_with_empty_enumberable_of_class_with_properties_then_data_returned()
        {
            var items = new TestClass2[0];

            var sut = new ReflectionTabulatorAdapter<TestClass2>(items);

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

            var sut = new ReflectionTabulatorAdapter<TestClass2>(items);

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

            var sut = new ReflectionTabulatorAdapter<TestClass2>(items, TypeMembers.Properties, AccessModifiers.NonPublic);

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

            var sut = new ReflectionTabulatorAdapter<TestClass1>(items, TypeMembers.Properties, AccessModifiers.PublicAndNonPublic);

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

            var sut = new ReflectionTabulatorAdapter<TestClass3>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

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

            var sut = new ReflectionTabulatorAdapter<TestClass5>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

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

            var sut = new ReflectionTabulatorAdapter<TestClass1>(items, TypeMembers.Fields, AccessModifiers.PublicAndNonPublic);

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

            var sut = new ReflectionTabulatorAdapter<TestClass6>(items, TypeMembers.PropertiesAndFields);

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

            var sut = new ReflectionTabulatorAdapter<TestClass6>(items, TypeMembers.PropertiesAndFields, AccessModifiers.NonPublic);

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

        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void When_called_with_empty_enumberable_of_struct_with_properties_then_data_returned()
        {
            var items = new TestStruct2[0];

            var sut = new ReflectionTabulatorAdapter<TestStruct2>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestStruct2.StringProperty), i),
                i => Assert.Equal(nameof(TestStruct2.IntProperty), i),
                i => Assert.Equal(nameof(TestStruct2.EnumerableProperty), i),
            });

            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_public_access_and_with_multiple_objects_of_struct_with_public_properties_then_data_returned()
        {
            var items = new TestStruct2[]
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

            var sut = new ReflectionTabulatorAdapter<TestStruct2>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestStruct2.StringProperty), i),
                i => Assert.Equal(nameof(TestStruct2.IntProperty), i),
                i => Assert.Equal(nameof(TestStruct2.EnumerableProperty), i),
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
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_struct_with_public_properties_then_data_returned()
        {
            var items = new TestStruct2[]
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

            var sut = new ReflectionTabulatorAdapter<TestStruct2>(items, TypeMembers.Properties, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_object_of_struct_with_no_properties_then_data_returned()
        {
            var items = new TestStruct1[]
            {
                new(),
            };

            var sut = new ReflectionTabulatorAdapter<TestStruct1>(items, TypeMembers.Properties, AccessModifiers.PublicAndNonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_struct_with_private_fields_then_data_returned()
        {
            var items = new TestStruct3[]
            {
                new( "Hello", 123, new string[] { "foo", "bar" }),
                new( "World", 456, new string[] { "goo", "baz" }),
                new( "Hello World", 789, new string[] { "noo", "buzz" }),
            };

            var sut = new ReflectionTabulatorAdapter<TestStruct3>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

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
        public void When_called_with_nonpublic_access_and_with_multiple_objects_of_struct_with_public_fields_then_data_returned()
        {
            var items = new TestStruct5[]
            {
                new(),
            };

            var sut = new ReflectionTabulatorAdapter<TestStruct5>(items, TypeMembers.Fields, AccessModifiers.NonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_multiple_objects_of_struct_with_no_fields_then_data_returned()
        {
            var items = new TestStruct1[]
            {
                new(),
            };

            var sut = new ReflectionTabulatorAdapter<TestStruct1>(items, TypeMembers.Fields, AccessModifiers.PublicAndNonPublic);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }

        [Fact]
        public void When_called_with_public_access_and_with_struct_with_public_properties_and_private_field_then_return_data()
        {
            var items = new TestStruct6[]
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

            var sut = new ReflectionTabulatorAdapter<TestStruct6>(items, TypeMembers.PropertiesAndFields);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestStruct2.StringProperty), i),
                i => Assert.Equal(nameof(TestStruct2.IntProperty), i),
                i => Assert.Equal(nameof(TestStruct2.EnumerableProperty), i),
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
        public void When_called_with_nonpublic_access_and_with_struct_with_public_properties_and_private_field_then_return_data()
        {
            var items = new TestStruct6[]
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

            var sut = new ReflectionTabulatorAdapter<TestStruct6>(items, TypeMembers.PropertiesAndFields, AccessModifiers.NonPublic);

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

        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void When_called_with_single_class_item_then_data_is_returned()
        {
            var item = new TestClass2
            {
                StringProperty = "Hello",
                IntProperty = 123,
                EnumerableProperty = new string[] { "foo", "bar" },
            };

            var sut = new ReflectionTabulatorAdapter<TestClass2>(item);

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
                    j => Assert.Equal(item.StringProperty, j),
                    j => Assert.Equal(item.IntProperty.ToString(), j),
                    j => Assert.Equal(item.EnumerableProperty.ToString(), j),
                }),
            });
        }
    }
}
