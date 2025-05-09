using System.Text;
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.Null(result.Lines);
            Assert.Equal(0, result.Width);
            Assert.Equal(0, result.Height);
        }

        [Fact]
        public void When_text_is_empty_then_celldata_is_returned()
        {
            var text = string.Empty;

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, string.Empty);
            Assert.Equal(0, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_has_single_line_then_celldata_is_returned()
        {
            var text = DataGenerator.GetString();

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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

            var sut = new CellDataParser(new TabulatorOptions());

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
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
            var sut = new CellDataParser(new TabulatorOptions());

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Parse(-1, 0, DataGenerator.GetString()));
        }

        [Fact]
        public void When_row_is_less_than_zero_then_throw_argumentoutofrangeexception()
        {
            var sut = new CellDataParser(new TabulatorOptions());

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Parse(0, -1, DataGenerator.GetString()));
        }

        [Fact]
        public void When_text_has_all_printable_characters_and_includenonprintablecharacters_is_true_then_celldata_is_returned()
        {
            var text = DataGenerator.GetString();

            var sut = new CellDataParser(new TabulatorOptions { IncludeNonPrintableCharacters = true });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, text);
            Assert.Equal(text.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_has_nonprintable_characters_and_includenonprintablecharacters_is_true_then_celldata_is_returned()
        {
            var text = DataGenerator.GetString();
            var nonPrintableChars = " " ; // U+2001 (Em Quad)
            var textWithNonPrintableChars = nonPrintableChars + text;

            var sut = new CellDataParser(new TabulatorOptions { IncludeNonPrintableCharacters = true });

            var result = sut.Parse(0, 0, textWithNonPrintableChars);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, textWithNonPrintableChars);
            Assert.Equal(textWithNonPrintableChars.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_has_nonprintable_characters_and_includenonprintablecharacters_is_false_then_celldata_is_returned()
        {
            var text = DataGenerator.GetString();
            var nonPrintableChars = " "; // U+2001 (Em Quad)
            var textWithNonPrintableChars = nonPrintableChars + text;

            var sut = new CellDataParser(new TabulatorOptions { IncludeNonPrintableCharacters = false });

            var result = sut.Parse(0, 0, textWithNonPrintableChars);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, text);
            Assert.Equal(text.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_contains_tabs_and_tablength_is_zero_then_tabs_are_not_replaced()
        {
            var text = "Column1\tColumn2\tColumn3";

            var sut = new CellDataParser(new TabulatorOptions { TabLength = 0 });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, text); // Tabs remain as-is
            Assert.Equal(text.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_contains_tabs_and_tablength_is_positive_then_tabs_are_replaced_with_spaces()
        {
            var tabLength = 4;
            var text = "Column1\tColumn2\tColumn3";
            var expectedText = "Column1    Column2    Column3"; // Tabs replaced with 4 spaces

            var sut = new CellDataParser(new TabulatorOptions { TabLength = tabLength });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, expectedText);
            Assert.Equal(expectedText.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_contains_tabs_and_tablength_is_positive_with_multiple_lines_then_tabs_are_replaced()
        {
            var tabLength = 2;
            var text = "Col1\tCol2\nCol3\tCol4";
            var expectedLines = new[] { "Col1  Col2", "Col3  Col4" }; // Tabs replaced with 2 spaces

            var sut = new CellDataParser(new TabulatorOptions { TabLength = tabLength });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Collection(result.Lines,
                line => Assert.Equal(expectedLines[0], line),
                line => Assert.Equal(expectedLines[1], line)
            );
            Assert.Equal(expectedLines[0].Length, result.Width); // Width of the longest line
            Assert.Equal(expectedLines.Length, result.Height);
        }

        [Fact]
        public void When_text_contains_only_tabs_then_tabs_are_replaced_with_spaces()
        {
            var tabLength = 3;
            var text = "\t\t\t";
            var expectedText = new string(' ', tabLength * 3); // Each tab replaced with 3 spaces

            var sut = new CellDataParser(new TabulatorOptions { TabLength = tabLength });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, expectedText);
            Assert.Equal(expectedText.Length, result.Width);
            Assert.Equal(1, result.Height);
        }

        [Fact]
        public void When_text_contains_tabs_and_tablength_is_negative_then_tabs_are_not_replaced()
        {
            var text = "Column1\tColumn2\tColumn3";
            var expected = text.Replace("\t", string.Empty);

            var sut = new CellDataParser(new TabulatorOptions { TabLength = -4 });

            var result = sut.Parse(0, 0, text);

            Assert.Equal(0, result.Column);
            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Lines);
            Assert.Single(result.Lines, expected); // Tabs remain as-is
            Assert.Equal(expected.Length, result.Width);
            Assert.Equal(1, result.Height);
        }
    }
}
