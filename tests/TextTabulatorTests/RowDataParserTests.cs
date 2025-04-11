using TextTabulator;
using TextTabulator.Testing;

namespace TextTabulatorTests
{
    public class RowDataParserTests
    {
        [Fact]
        public void When_cells_is_null_then_return_rowdata()
        {
            IEnumerable<string>? cells = null;

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.Null(result.Cells);
            Assert.Equal(0, result.MaxHeight);
        }

        [Fact]
        public void When_cells_is_empty_then_return_rowdata()
        {
            var cells = Array.Empty<string>();

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Empty(result.Cells);
            Assert.Equal(1, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_one_single_line_cell_then_return_rowdata()
        {
            var count = 1;
            var cells = DataGenerator.GetStrings(count);

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Single(result.Cells);
            Assert.Equal(1, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_multiple_single_line_cells_then_return_rowdata()
        {
            var count = 3;
            var cells = DataGenerator.GetStrings(count);

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(count, result.Cells.Count);
            Assert.Equal(1, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_single_multiple_line_cell_then_return_rowdata()
        {
            var cellCount = 1;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(cellCount * lineCount);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}"
            };

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_same_height_then_return_rowdata()
        {
            var cellCount = 3;
            var lineCount = 3;
            var lines = DataGenerator.GetStrings(cellCount * lineCount);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}",
                $"{lines[(1 * lineCount) + 0]}\n{lines[(1 * lineCount) + 1]}\n{lines[(1 * lineCount) + 2]}",
                $"{lines[(2 * lineCount) + 0]}\n{lines[(2 * lineCount) + 1]}\n{lines[(2 * lineCount) + 2]}"
            };

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(lineCount, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_first_having_the_tallest_height_then_return_rowdata()
        {
            var cellCount = 3;
            var lines = DataGenerator.GetStrings(7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}\n{lines[2]}",
                $"{lines[3]}\n{lines[4]}",
                $"{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(3, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_middle_having_the_tallest_height_then_return_rowdata()
        {
            var cellCount = 3;
            var lines = DataGenerator.GetStrings(7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}",
                $"{lines[2]}\n{lines[3]}\n{lines[4]}",
                $"{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(3, result.MaxHeight);
        }

        [Fact]
        public void When_cells_has_multiple_multiple_line_cells_with_the_last_having_the_tallest_height_then_return_rowdata()
        {
            var cellCount = 3;
            var lines = DataGenerator.GetStrings(7);
            var cells = new string[]
            {
                $"{lines[0]}\n{lines[1]}",
                $"{lines[2]}\n{lines[3]}",
                $"{lines[4]}\n{lines[5]}\n{lines[6]}"
            };

            var sut = new RowDataParser();

            var result = sut.Parse(0, cells);

            Assert.Equal(0, result.Row);
            Assert.NotNull(result.Cells);
            Assert.Equal(cellCount, result.Cells.Count);
            Assert.Equal(3, result.MaxHeight);
        }
    }
}
