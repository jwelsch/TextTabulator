using TextTabulator.Adapters.Json;

namespace TextTabulator.Adapters.JsonTests
{
    public class SnakeJsonPropertyNameTransformTests
    {
        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_lower_case_then_return_transformed_name()
        {
            var name = "foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(true, false, null);

            var result = sut.Apply(name);

            Assert.Equal("Foo_bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "Foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(true, false, null);

            var result = sut.Apply(name);

            Assert.Equal("Foo_bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_lower_case_then_return_transformed_name()
        {
            var name = "foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(false, true, null);

            var result = sut.Apply(name);

            Assert.Equal("foo_Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_upper_case_then_return_transformed_name()
        {
            var name = "foo_Bar";

            var sut = new SnakeJsonPropertyNameTransform(false, true, null);

            var result = sut.Apply(name);

            Assert.Equal("foo_Bar", result);
        }

        [Fact]
        public void When_underscore_replacement_is_not_null_and_underscores_present_in_property_name_then_return_transformed_name()
        {
            var name = "foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(false, false, '_');

            var result = sut.Apply(name);

            Assert.Equal("foo_bar", result);
        }

        [Fact]
        public void When_underscore_replacement_is_not_null_and_underscorees_not_present_in_property_name_then_return_transformed_name()
        {
            var name = "foobar";

            var sut = new SnakeJsonPropertyNameTransform(false, false, '_');

            var result = sut.Apply(name);

            Assert.Equal("foobar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_lower_case_and_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_lower_case_and_underscore_replacement_is_not_null_and_underscores_present_in_property_name_then_return_transformed_name()
        {
            var name = "foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_and_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_upper_case_and_underscore_replacement_is_not_null_and_underscores_not_present_in_property_name_then_return_transformed_name()
        {
            var name = "FooBar";

            var sut = new SnakeJsonPropertyNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("FooBar", result);
        }

        [Fact]
        public void When_number_is_first_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "123_foo_bar";

            var sut = new SnakeJsonPropertyNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("123 Foo Bar", result);
        }

        [Fact]
        public void When_number_is_in_middle_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "foo_123_bar";

            var sut = new SnakeJsonPropertyNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo 123 Bar", result);
        }

        [Fact]
        public void When_number_is_last_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "foo_bar_123";

            var sut = new SnakeJsonPropertyNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar 123", result);
        }
    }
}
