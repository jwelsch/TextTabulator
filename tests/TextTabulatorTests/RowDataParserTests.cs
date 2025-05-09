using TextTabulator;
using TextTabulator.Testing;

namespace TextTabulatorTests
{
    public class RowDataParserTests
    {
        [Fact]
        public void When_cells_is_null_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            IEnumerable<string>? cells = null;

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.Null(result.Cells);
            Assert.Equal(0, result.MaxHeight);
        }

        [Fact]
        public void When_cells_is_empty_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var lineCount = 1;
            var cells = Array.Empty<string>();

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Empty(result.Cells);
            Assert.Equal(lineCount, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_one_single_line_cell_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var count = 1;
            var lineCount = 1;
            var row = 3;
            var cells = DataGenerator.GetStrings(length, count);

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(row, cells, ref maxWidths);

            Assert.Equal(row, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Single(result.Cells);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Single(maxWidths, length);
        }

        [Fact]
        public void When_cells_has_multiple_single_line_cells_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var count = 3;
            var lineCount = 1;
            var cells = DataGenerator.GetStrings(length, count);

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(count, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i)
            );
        }

        [Fact]
        public void When_cells_has_single_multiple_line_cell_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var cellCount = 1;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(length, cellCount * lineCount);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}"
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Single(maxWidths, length);
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_same_height_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var cellCount = 3;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(length, cellCount * lineCount);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}",
                $"{lines[(1 * lineCount) + 0]}\n{lines[(1 * lineCount) + 1]}\n{lines[(1 * lineCount) + 2]}",
                $"{lines[(2 * lineCount) + 0]}\n{lines[(2 * lineCount) + 1]}\n{lines[(2 * lineCount) + 2]}"
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_first_having_the_tallest_height_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(length, 7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}",
                $"{lines[3]}\n{lines[4]}",
                $"{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_middle_having_the_tallest_height_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(length, 7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}",
                $"{lines[2]}\n{lines[3]}\n{lines[4]}",
                $"{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_last_having_the_tallest_height_then_return_rowdata()
        {
            var maxWidths = new List<int>();
            var length = 10;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(length, 7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}",
                $"{lines[2]}\n{lines[3]}",
                $"{lines[4]}\n{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i),
                i => Assert.Equal(length, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_single_line_cells_and_the_first_maxwidths_index_changes_then_return_rowdata()
        {
            var changingWidth = 9;
            var unchangingWidth = 10;
            var maxWidths = new List<int> { changingWidth, unchangingWidth, unchangingWidth };
            var length0 = 10;
            var length1 = 9;
            var length2 = 9;
            var lineCount = 1;
            var cells = new string[]
            {
                DataGenerator.GetString(length0),
                DataGenerator.GetString(length1),
                DataGenerator.GetString(length2)
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(length0, i),
                i => Assert.Equal(unchangingWidth, i),
                i => Assert.Equal(unchangingWidth, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_single_line_cells_and_the_middle_maxwidths_index_changes_then_return_rowdata()
        {
            var changingWidth = 9;
            var unchangingWidth = 10;
            var maxWidths = new List<int> { unchangingWidth, changingWidth, unchangingWidth };
            var length0 = 9;
            var length1 = 10;
            var length2 = 9;
            var lineCount = 1;
            var cells = new string[]
            {
                DataGenerator.GetString(length0),
                DataGenerator.GetString(length1),
                DataGenerator.GetString(length2)
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(unchangingWidth, i),
                i => Assert.Equal(length1, i),
                i => Assert.Equal(unchangingWidth, i)
            );
        }

        [Fact]
        public void When_cells_has_multiple_single_line_cells_and_the_last_maxwidths_index_changes_then_return_rowdata()
        {
            var changingWidth = 9;
            var unchangingWidth = 10;
            var maxWidths = new List<int> { unchangingWidth, unchangingWidth, changingWidth };
            var length0 = 9;
            var length1 = 9;
            var length2 = 10;
            var lineCount = 1;
            var cells = new string[]
            {
                DataGenerator.GetString(length0),
                DataGenerator.GetString(length1),
                DataGenerator.GetString(length2)
            };

            var sut = new RowDataParser(new TabulatorOptions());

            var result = sut.Parse(0, cells, ref maxWidths);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cells.Length, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
            Assert.Collection(maxWidths,
                i => Assert.Equal(unchangingWidth, i),
                i => Assert.Equal(unchangingWidth, i),
                i => Assert.Equal(length2, i)
            );
        }
    }
}
