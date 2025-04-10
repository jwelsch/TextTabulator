using TextTabulator;
using TextTabulator.Testing;

namespace TextTabulatorTests
{
    public class CellDataParserTests
    {
        [Fact]
        public void When_text_is_null_then_celldata_is_returned()
        {
            string? text = null;

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Empty(result.Lines);
            Assert.Equal(0, result.Width);
            Assert.Equal(0, result.Height);
        }

        [Fact]
        public void When_text_is_empty_then_celldata_is_returned()
        {
            var text = string.Empty;

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Empty(result.Lines);
            Assert.Equal(0, result.Width);
            Assert.Equal(0, result.Height);
        }

        [Fact]
        public void When_text_has_single_line_then_celldata_is_returned()
        {
            var text = DataGenerator.GetString();

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Single(result.Lines, text);
            Assert.Equal(text.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_of_equal_length_then_celldata_is_returned()
        {
            var count = 3;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\n{specimins[1]}\n{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_with_first_longer_then_celldata_is_returned()
        {
            var maxLength = 9;
            var minLength = 6;
            var specimins = new string[]
            {
                DataGenerator.GetString(maxLength),
                DataGenerator.GetString(minLength),
                DataGenerator.GetString(minLength),
            };
            var text = $"{specimins[0]}\n{specimins[1]}\n{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(maxLength, result.Width);
            Assert.Equal(specimins.Length, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_with_middle_longer_then_celldata_is_returned()
        {
            var maxLength = 9;
            var minLength = 6;
            var specimins = new string[]
            {
                DataGenerator.GetString(minLength),
                DataGenerator.GetString(maxLength),
                DataGenerator.GetString(minLength),
            };
            var text = $"{specimins[0]}\n{specimins[1]}\n{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(maxLength, result.Width);
            Assert.Equal(specimins.Length, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_with_last_longer_then_celldata_is_returned()
        {
            var maxLength = 9;
            var minLength = 6;
            var specimins = new string[]
            {
                DataGenerator.GetString(minLength),
                DataGenerator.GetString(minLength),
                DataGenerator.GetString(maxLength),
            };
            var text = $"{specimins[0]}\n{specimins[1]}\n{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(maxLength, result.Width);
            Assert.Equal(specimins.Length, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_carriage_return_then_celldata_is_returned()
        {
            var count = 3;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\r{specimins[1]}\r{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_new_line_carriage_return_then_celldata_is_returned()
        {
            var count = 3;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\r\n{specimins[1]}\r\n{specimins[2]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_new_line_carriage_return_and_new_line_and_carriage_return_then_celldata_is_returned()
        {
            var count = 4;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\r\n{specimins[1]}\n{specimins[2]}\r{specimins[3]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i),
                i => Assert.Equal(specimins[3], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_carriage_return_and_new_line_carriage_return_and_new_line_then_celldata_is_returned()
        {
            var count = 4;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\r{specimins[1]}\r\n{specimins[2]}\n{specimins[3]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i),
                i => Assert.Equal(specimins[3], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_new_line_and_carriage_return_and_new_line_carriage_return_then_celldata_is_returned()
        {
            var count = 4;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\n{specimins[1]}\r{specimins[2]}\r\n{specimins[3]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i),
                i => Assert.Equal(specimins[3], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_text_multiple_lines_separated_by_new_line_carriage_return_and_carriage_return_and_new_line_then_celldata_is_returned()
        {
            var count = 4;
            var specimins = DataGenerator.GetStrings(count);
            var text = $"{specimins[0]}\r\n{specimins[1]}\r{specimins[2]}\n{specimins[3]}";

            var sut = new CellDataParser();

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Collection(result.Lines,
                i => Assert.Equal(specimins[0], i),
                i => Assert.Equal(specimins[1], i),
                i => Assert.Equal(specimins[2], i),
                i => Assert.Equal(specimins[3], i)
            );
            Assert.Equal(specimins[0].Length, result.Width);
            Assert.Equal(count, result.Height);
        }

        [Fact]
        public void When_column_is_less_than_zero_then_throw_argumentoutofrangeexception()
        {
            var sut = new CellDataParser();

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Parse(-1, 0, DataGenerator.GetString()));
        }

        [Fact]
        public void When_row_is_less_than_zero_then_throw_argumentoutofrangeexception()
        {
            var sut = new CellDataParser();

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Parse(0, -1, DataGenerator.GetString()));
        }
    }
}
