using TextTabulator.Adapters;

namespace TextTabulator.AdaptersTests
{
    public class TypeFormatterTests
    {
        [Fact]
        public void When_given_int_and_using_default_formatter_then_return_formatted()
        {
            var input = 123;

            var sut = new TypeFormatter();

            var result = sut.FormatTypeValue(input);

            Assert.Equal("123", result);
        }

        [Fact]
        public void When_given_int_and_using_custom_formatter_then_return_formatted()
        {
            var input = 123;

            var sut = new TypeFormatter(new Dictionary<Type, Func<object, string>>
            {
                { input.GetType(), v => $"Custom: {v}" }
            });

            var result = sut.FormatTypeValue(input);

            Assert.Equal($"Custom: {input}", result);
        }

        [Theory]
#pragma warning disable xUnit1042 // The member referenced by the MemberData attribute returns untyped data rows
        [MemberData(nameof(Data))]
#pragma warning restore xUnit1042 // The member referenced by the MemberData attribute returns untyped data rows
        public void When_given_value_and_using_default_formatter_then_return_formatted(object? input)
        {
            var sut = new TypeFormatter();

            var result = sut.FormatTypeValue(input);

            Assert.Equal(input?.ToString() ?? "", result);
        }

        [Theory]
#pragma warning disable xUnit1042 // The member referenced by the MemberData attribute returns untyped data rows
        [MemberData(nameof(Data))]
#pragma warning restore xUnit1042 // The member referenced by the MemberData attribute returns untyped data rows
        public void When_given_value_and_using_custom_formatter_then_return_formatted(object? input)
        {
            var sut = new TypeFormatter(new Dictionary<Type, Func<object, string>>
            {
                { input?.GetType() ?? typeof(object), v => $"Custom: {v}" }
            });

            var result = sut.FormatTypeValue(input);

            Assert.Equal(input == null ? "" : $"Custom: {input}", result);
        }

        public static IEnumerable<object?[]> Data =>
            new List<object?[]>
            {
                    new object?[] { 123 },
                    new object?[] { "hello" },
                    new object?[] { new DateTime(2025, 11, 9) },
                    new object?[] { null },
            };
    }
}
