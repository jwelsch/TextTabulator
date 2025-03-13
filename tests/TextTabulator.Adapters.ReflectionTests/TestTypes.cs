namespace TextTabulator.Adapters.ReflectionTests
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

    internal struct TestStruct1
    {
    }

    internal struct TestStruct2
    {
        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestStruct2()
        {
        }
    }

    internal readonly struct TestStruct3
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly string _stringField;
        private readonly int _intField;
        private readonly IEnumerable<string> _enumerableField;
#pragma warning restore IDE0052 // Remove unread private members

        public TestStruct3(string stringField, int intField, IEnumerable<string> enumerableField)
        {
            _stringField = stringField;
            _intField = intField;
            _enumerableField = enumerableField;
        }
    }

    internal struct TestStruct4
    {
        private string StringProperty { get; set; } = string.Empty;

        private int IntProperty { get; set; }

        private IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestStruct4()
        {
        }
    }

    internal readonly struct TestStruct5
    {
        public readonly string _stringField = string.Empty;
#pragma warning disable CS0649
        public readonly int _intField;
#pragma warning restore CS0649
        public readonly IEnumerable<string> _enumerableField = Array.Empty<string>();

        public TestStruct5()
        {
        }
    }

    internal struct TestStruct6
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly string _stringField;
        private readonly int _intField;
        private readonly IEnumerable<string> _enumerableField;
#pragma warning restore IDE0052 // Remove unread private members

        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestStruct6(string stringField, int intField, IEnumerable<string> enumerableField)
        {
            _stringField = stringField;
            _intField = intField;
            _enumerableField = enumerableField;
        }
    }
}