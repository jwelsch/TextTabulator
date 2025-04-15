using TextTabulator.Adapters.Json;

namespace TextTabulator.Adapters.JsonTests
{
    public class CamelNameTransformTests
    {
        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "fooBar";

            var sut = new CamelNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_false_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "FooBar";

            var sut = new CamelNameTransform(false, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("foo Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_subsequent_word_is_false_and_two_words_exist_then_return_transformed_name()
        {
            var name = "fooBar";

            var sut = new CamelNameTransform(true, false, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_false_and_first_letter_of_first_word_is_upper_case_and_capitalize_first_letter_of_subsequent_word_is_false_and_two_words_exist_then_return_transformed_name()
        {
            var name = "fooBar";

            var sut = new CamelNameTransform(false, false, ' ');

            var result = sut.Apply(name);

            Assert.Equal("foo bar", result);
        }

        [Fact]
        public void When_number_is_first_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "123FooBar";

            var sut = new CamelNameTransform(true, true);

            var result = sut.Apply(name);

            Assert.Equal("123 Foo Bar", result);
        }

        [Fact]
        public void When_number_is_in_middle_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "foo123Bar";

            var sut = new CamelNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo 123 Bar", result);
        }

        [Fact]
        public void When_number_is_last_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "fooBar123";

            var sut = new CamelNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar 123", result);
        }

        [Fact]
        public void When_separator_is_null_then_return_transformed_name()
        {
            var name = "fooBar";

            var sut = new CamelNameTransform(true, true, null);

            var result = sut.Apply(name);

            Assert.Equal("FooBar", result);
        }

        [Fact]
        public void When_number_is_in_middle_separator_is_null_then_return_transformed_name()
        {
            var name = "foo123Bar";

            var sut = new CamelNameTransform(true, true, null);

            var result = sut.Apply(name);

            Assert.Equal("Foo123Bar", result);
        }
    }
}
