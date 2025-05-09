using TextTabulator;
using TextTabulator.Testing;

namespace TextTabulatorTests
{
    public class TableDataParserTests
    {
        [Fact]
        public void When_headers_are_null_and_rows_are_null_then_return_tabledata()
        {
            IEnumerable<string>? headers = null;
            IEnumerable<IEnumerable<string>>? rows = null;

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.Null(result.Headers);
            Assert.Null(result.ValueRows);
        }

        [Fact]
        public void When_headers_are_empty_and_rows_are_emtpy_then_return_tabledata()
        {
            var headers = Array.Empty<string>();
            var rows = Array.Empty<string[]>();

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Empty(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.Empty(result.ValueRows);
        }

        [Fact]
        public void When_headers_are_null_and_rows_are_emtpy_then_return_tabledata()
        {
            IEnumerable<string>? headers = null;
            var rows = Array.Empty<string[]>();

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.Null(result.Headers);
            Assert.NotNull(result.ValueRows);
            Assert.Empty(result.ValueRows);
        }

        [Fact]
        public void When_headers_are_empty_and_rows_are_null_then_return_tabledata()
        {
            var headers = Array.Empty<string>();
            IEnumerable<IEnumerable<string>>? rows = null;

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Empty(result.Headers.Cells);
            Assert.Null(result.ValueRows);
        }

        [Fact]
        public void When_headers_has_single_cell_and_rows_are_emtpy_then_return_tabledata()
        {
            var headers = DataGenerator.GetStrings(1);
            var rows = Array.Empty<string[]>();

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Single(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.Empty(result.ValueRows);
        }

        [Fact]
        public void When_headers_has_multiple_cells_and_rows_are_emtpy_then_return_tabledata()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength, cellLength, cellLength };
            var headers = DataGenerator.GetStrings(cellLengths);
            var rows = Array.Empty<string[]>();

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.Empty(result.ValueRows);
        }

        [Fact]
        public void When_headers_are_empty_and_rows_has_single_cell_then_return_tabledata()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength };
            var headers = Array.Empty<string>();
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Empty(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.Single(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);
            Assert.Single(cells);
        }

        [Fact]
        public void When_headers_are_empty_and_rows_has_multiple_cells_then_return_tabledata()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength, cellLength, cellLength };
            var headers = Array.Empty<string>();
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Empty(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);
        }

        [Fact]
        public void When_headers_has_more_cells_than_rows_then_throw_invalidoperationexception()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength, cellLength, cellLength };
            var headers = DataGenerator.GetStrings(cellLengths.Length + 1);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            Assert.Throws<InvalidOperationException>(() => sut.Parse(headers, rows));
        }

        [Fact]
        public void When_headers_has_less_cells_than_rows_then_throw_invalidoperationexception()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength, cellLength, cellLength };
            var headers = DataGenerator.GetStrings(cellLengths.Length - 1);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            Assert.Throws<InvalidOperationException>(() => sut.Parse(headers, rows));
        }

        [Fact]
        public void When_headers_has_one_cell_and_rows_has_one_cell_has_then_return_tabledata()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength };
            var headers = DataGenerator.GetStrings(cellLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.Single(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);
            Assert.Single(cells);
        }

        [Fact]
        public void When_headers_has_multiple_cells_and_rows_has_multiple_cells_then_return_tabledata()
        {
            var cellLength = 10;
            var cellLengths = new int[] { cellLength, cellLength, cellLength };
            var headers = DataGenerator.GetStrings(cellLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(cellLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);

        }

        [Fact]
        public void When_headers_has_longest_cells_and_rows_has_single_row_then_return_tabledata()
        {
            var headerLength = 11;
            var valueLength = 10;
            var headerLengths = new int[] { headerLength, headerLength, headerLength };
            var valueLengths = new int[] { valueLength, valueLength, valueLength };
            var headers = DataGenerator.GetStrings(headerLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(valueLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);
        }

        [Fact]
        public void When_headers_has_shortest_cells_and_rows_has_single_row_then_return_tabledata()
        {
            var headerLength = 10;
            var valueLength = 11;
            var headerLengths = new int[] { headerLength, headerLength, headerLength };
            var valueLengths = new int[] { valueLength, valueLength, valueLength };
            var headers = DataGenerator.GetStrings(headerLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(valueLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            var cells = result.ValueRows[0].Cells;
            Assert.NotNull(cells);
        }

        [Fact]
        public void When_headers_has_longest_cells_and_rows_has_multiple_rows_then_return_tabledata()
        {
            var headerLength = 11;
            var valueLength = 10;
            var headerLengths = new int[] { headerLength, headerLength, headerLength };
            var valueLengths = new int[] { valueLength, valueLength, valueLength };
            var headers = DataGenerator.GetStrings(headerLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(valueLengths),
                DataGenerator.GetStrings(valueLengths),
                DataGenerator.GetStrings(valueLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.NotNull(result.ValueRows[0].Cells);
            Assert.NotNull(result.ValueRows[1].Cells);
            Assert.NotNull(result.ValueRows[2].Cells);
        }

        [Fact]
        public void When_headers_has_shortest_cells_and_rows_has_multiple_rows_then_return_tabledata()
        {
            var headerLength = 10;
            var valueLength = 11;
            var headerLengths = new int[] { headerLength, headerLength, headerLength };
            var valueLengths = new int[] { valueLength, valueLength, valueLength };
            var headers = DataGenerator.GetStrings(headerLengths);
            var rows = new string[][]
            {
                DataGenerator.GetStrings(valueLengths),
                DataGenerator.GetStrings(valueLengths),
                DataGenerator.GetStrings(valueLengths)
            };

            var sut = new TableDataParser(new TabulatorOptions());

            var result = sut.Parse(headers, rows);

            Assert.NotNull(result.Headers);
            Assert.NotNull(result.Headers.Cells);
            Assert.NotNull(result.ValueRows);
            Assert.NotNull(result.ValueRows[0].Cells);
            Assert.NotNull(result.ValueRows[1].Cells);
            Assert.NotNull(result.ValueRows[2].Cells);
        }
    }
}
