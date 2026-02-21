using TextTabulator.Adapters;

namespace TextTabulator.Testing
{
    public enum TestEnum
    {
        None,
        First,
        Second,
    }

    public record TestRecord1(string StringProperty, int IntProperty, IEnumerable<string> EnumerableProperty);

    public record struct TestRecordStruct1(string StringProperty, int IntProperty, IEnumerable<string> EnumerableProperty);

    public interface ITestInterface1
    {
        string StringProperty { get; set; }

        int IntProperty { get; set; }

        IEnumerable<string> EnumerableProperty { get; set; }
    }

    public delegate void TestDelegate1(string stringParameter, int intParameter, IEnumerable<string> enumerableParameter);

    public class TestClass1
    {
    }

    public class TestClass2
    {
        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();
    }

    public class TestClass3
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

    public class TestClass4
    {
        protected string StringProperty { get; set; } = string.Empty;

        protected int IntProperty { get; set; }

        protected IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();
    }

    public class TestClass5
    {
        public readonly string _stringField = string.Empty;
#pragma warning disable CS0649
        public readonly int _intField;
#pragma warning restore CS0649
        public readonly IEnumerable<string> _enumerableField = Array.Empty<string>();
    }

    public class TestClass6
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

    public class TestClass7
    {
#pragma warning disable IDE0052 // Remove unread private members
        [TabulatorIgnore]
        private readonly string _stringField;
        private readonly int _intField;
#pragma warning restore IDE0052 // Remove unread private members

        public string StringProperty { get; set; } = string.Empty;

        [TabulatorIgnore]
        public int IntProperty { get; set; }

        public TestClass7(string stringField, int intField)
        {
            _stringField = stringField;
            _intField = intField;
        }
    }

    public class TestClass8
    {
        public DateTime DateTimeProperty { get; set; } = DateTime.Now;
    }

    public class TestClass9
    {
        public string PublicField = string.Empty;

        protected string ProtectedField = string.Empty;

        private string PrivateField = string.Empty;

        public string PublicProperty { get; set; } = string.Empty;

        protected string ProtectedProperty { get; set; } = string.Empty;

        private string PrivateProperty { get; set; } = string.Empty;

        public TestClass9()
        {
        }

        public TestClass9(string protectedField, string privateField, string protectedProperty, string privateProperty)
        {
            ProtectedField = protectedField;
            PrivateField = privateField;
            ProtectedProperty = protectedProperty;
            PrivateProperty = privateProperty;
        }

        public void PublicMethod()
        {
        }

        protected void ProtectedMethod()
        {
        }

        private void PrivateMethod()
        {
        }

        [TabulatorIgnore]
        public string IgnoredPublicProperty { get; set; } = string.Empty;
    }

    public struct TestStruct1
    {
    }

    public struct TestStruct2
    {
        public string StringProperty { get; set; } = string.Empty;

        public int IntProperty { get; set; }

        public IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestStruct2()
        {
        }
    }

    public readonly struct TestStruct3
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

    public struct TestStruct4
    {
        private string StringProperty { get; set; } = string.Empty;

        private int IntProperty { get; set; }

        private IEnumerable<string> EnumerableProperty { get; set; } = Array.Empty<string>();

        public TestStruct4()
        {
        }
    }

    public readonly struct TestStruct5
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

    public struct TestStruct6
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

    public struct TestStruct7
    {
#pragma warning disable IDE0052 // Remove unread private members
        [TabulatorIgnore]
        private readonly string _stringField;
        private readonly int _intField;
#pragma warning restore IDE0052 // Remove unread private members

        public string StringProperty { get; set; } = string.Empty;

        [TabulatorIgnore]
        public int IntProperty { get; set; }

        public TestStruct7(string stringField, int intField)
        {
            _stringField = stringField;
            _intField = intField;
        }
    }

    public class TextClassWithNullable
    {
        public string? StringProperty { get; set; }

        public int? IntProperty { get; set; }

        public DateTime? DateTimeProperty { get; set; }

        public IEnumerable<int?>? EnumerableProperty { get; set; }
    }
}