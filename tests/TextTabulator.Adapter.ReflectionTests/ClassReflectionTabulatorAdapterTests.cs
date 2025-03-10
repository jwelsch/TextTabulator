using TextTabulator.Adapter.Reflection;

namespace TextTabulator.Adapter.ReflectionTests
{
    internal class TestClass
    {
        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();
    }

    internal class TestClass2
    {
        private string _stringField = string.Empty;

        private int _intField;

        private IEnumerable<string> enumerableField = Array.Empty<string>();
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
        public void When_called_with_one_object_of_class_with_properties_then_data_returned()
        {
            var items = new TestClass[]
            {
                new()
                {
                    StringProperty = "Hello",
                    IntProperty = 123,
                    EnumerableProperty = new string[] { "foo", "bar" },
                },
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass.StringProperty), i),
                i => Assert.Equal(nameof(TestClass.IntProperty), i),
                i => Assert.Equal(nameof(TestClass.EnumerableProperty), i),
            });

            Assert.Collection(values, new Action<IEnumerable<string>>[]
            {
                i => Assert.Collection(i, new Action<string>[]
                {
                    j => Assert.Equal(items[0].StringProperty, j),
                    j => Assert.Equal(items[0].IntProperty.ToString(), j),
                    j => Assert.Equal(items[0].EnumerableProperty.ToString(), j),
                }),
            });
        }

        [Fact]
        public void When_called_with_multiple_objects_of_class_with_properties_then_data_returned()
        {
            var items = new TestClass[]
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

            var sut = new ClassReflectionTabulatorAdapter<TestClass>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.NotNull(headers);
            Assert.Collection(headers, new Action<string>[]
            {
                i => Assert.Equal(nameof(TestClass.StringProperty), i),
                i => Assert.Equal(nameof(TestClass.IntProperty), i),
                i => Assert.Equal(nameof(TestClass.EnumerableProperty), i),
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
        public void When_called_with_object_of_class_with_no_properties_then_data_returned()
        {
            var items = new TestClass2[]
            {
                new(),
            };

            var sut = new ClassReflectionTabulatorAdapter<TestClass2>(items);

            var headers = sut.GetHeaderStrings();
            var values = sut.GetValueStrings();

            Assert.Null(headers);
            Assert.Empty(values);
        }
    }
}
