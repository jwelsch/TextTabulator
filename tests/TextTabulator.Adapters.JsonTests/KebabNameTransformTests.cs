using TextTabulator.Adapters.Json;

namespace TextTabulator.Adapters.JsonTests
{
    public class KebabNameTransformTests
    {
        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_lower_case_then_return_transformed_name()
        {
            var name = "foo-bar";

            var sut = new KebabNameTransform(true, false, null);

            var result = sut.Apply(name);

            Assert.Equal("Foo-bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "Foo-bar";

            var sut = new KebabNameTransform(true, false, null);

            var result = sut.Apply(name);

            Assert.Equal("Foo-bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_lower_case_then_return_transformed_name()
        {
            var name = "foo-bar";

            var sut = new KebabNameTransform(false, true, null);

            var result = sut.Apply(name);

            Assert.Equal("foo-Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_upper_case_then_return_transformed_name()
        {
            var name = "foo-Bar";

            var sut = new KebabNameTransform(false, true, null);

            var result = sut.Apply(name);

            Assert.Equal("foo-Bar", result);
        }

        [Fact]
        public void When_dash_replacement_is_not_null_and_dashes_present_in_property_name_then_return_transformed_name()
        {
            var name = "foo-bar";

            var sut = new KebabNameTransform(false, false, '_');

            var result = sut.Apply(name);

            Assert.Equal("foo_bar", result);
        }

        [Fact]
        public void When_dash_replacement_is_not_null_and_dashes_not_present_in_property_name_then_return_transformed_name()
        {
            var name = "foobar";

            var sut = new KebabNameTransform(false, false, '_');

            var result = sut.Apply(name);

            Assert.Equal("foobar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_lower_case_and_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_lower_case_and_dash_replacement_is_not_null_and_dashes_present_in_property_name_then_return_transformed_name()
        {
            var name = "foo-bar";

            var sut = new KebabNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar", result);
        }

        [Fact]
        public void When_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_and_capitalize_first_letter_of_subsequent_words_is_true_and_first_letter_of_subsequent_words_are_upper_case_and_dash_replacement_is_not_null_and_dashes_not_present_in_property_name_then_return_transformed_name()
        {
            var name = "FooBar";

            var sut = new KebabNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("FooBar", result);
        }

        [Fact]
        public void When_number_is_first_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "123-foo-bar";

            var sut = new KebabNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("123 Foo Bar", result);
        }

        [Fact]
        public void When_number_is_in_middle_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "foo-123-bar";

            var sut = new KebabNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo 123 Bar", result);
        }

        [Fact]
        public void When_number_is_last_and_capitalize_first_letter_of_first_word_is_true_and_first_letter_of_first_word_is_upper_case_then_return_transformed_name()
        {
            var name = "foo-bar-123";

            var sut = new KebabNameTransform(true, true, ' ');

            var result = sut.Apply(name);

            Assert.Equal("Foo Bar 123", result);
        }
    }
}
