using TextTabulator;

namespace TextTabulatorTests
{
    public class TabulatorTests
    {
        [Fact]
        public void When_tabulate_called_with_one_header_and_one_row_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal($"--------\r\n|{headers[0]}|\r\n|------|\r\n|{values[0][0]} |\r\n--------\r\n", table);
        }

        [Fact]
        public void When_column_separator_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnSeparator = '#'
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"--------\r\n#{headers[0]}#\r\n|------|\r\n#{values[0][0]} #\r\n--------\r\n", table);
        }

        [Fact]
        public void When_column_left_padding_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnLeftPadding = "#"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"---------\r\n|#{headers[0]}|\r\n|-------|\r\n|#{values[0][0]} |\r\n---------\r\n", table);
        }

        [Fact]
        public void When_column_left_padding_customized_with_multicharacter_string_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions()
            {
                ColumnLeftPadding = "__"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"----------\r\n|__{headers[0]}|\r\n|--------|\r\n|__{values[0][0]} |\r\n----------\r\n", table);
        }

        [Fact]
        public void When_column_right_padding_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnRightPadding = "#"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"---------\r\n|{headers[0]}#|\r\n|-------|\r\n|{values[0][0]} #|\r\n---------\r\n", table);
        }

        [Fact]
        public void When_column_right_padding_customized_with_multicharacter_string_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnRightPadding = "__"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"----------\r\n|{headers[0]}__|\r\n|--------|\r\n|{values[0][0]} __|\r\n----------\r\n", table);
        }

        [Fact]
        public void When_column_left_padding_and_right_padding_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnLeftPadding = "@",
                ColumnRightPadding = "#"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"----------\r\n|@{headers[0]}#|\r\n|--------|\r\n|@{values[0][0]} #|\r\n----------\r\n", table);
        }

        [Fact]
        public void When_column_left_padding_with_multicharacter_string_and_right_padding_with_multicharacter_string_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                ColumnLeftPadding = "@@@",
                ColumnRightPadding = "###"
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"--------------\r\n|@@@{headers[0]}###|\r\n|------------|\r\n|@@@{values[0][0]} ###|\r\n--------------\r\n", table);
        }

        [Fact]
        public void When_tabulate_called_with_one_header_and_multiple_rows_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value1" },
                new string [] { "value2" },
                new string [] { "value3" },
            };

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal($"--------\r\n|{headers[0]}|\r\n|------|\r\n|{values[0][0]}|\r\n|------|\r\n|{values[1][0]}|\r\n|------|\r\n|{values[2][0]}|\r\n--------\r\n", table);
        }

        [Fact]
        public void When_tabulate_called_with_multiple_headers_and_multiple_rows_then_table_returned()
        {
            var headers = new string[]
            {
                "Header1",
                "Header2",
                "Header3",
            };

            var values = new string[][]
            {
                new string [] { "value1A", "value1B", "value1C" },
                new string [] { "value2A", "value2B", "value2C" },
                new string [] { "value3A", "value3B", "value3C" },
            };

            var expected =
@$"-------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
|-----------------------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-----------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-----------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_row_separator_customized_then_table_returned()
        {
            var headers = new string[]
            {
                "Header"
            };

            var values = new string[][]
            {
                new string [] { "value" }
            };

            var sut = new Tabulator();

            var options = new TabulatorOptions
            {
                RowSeparator = '_',
                LeftEdgeJoint = '_',
                RightEdgeJoint = '_',
                TopLeftCorner = '_',
                TopRightCorner = '_',
                BottomLeftCorner = '_',
                BottomRightCorner = '_',
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"________\r\n|{headers[0]}|\r\n________\r\n|{values[0][0]} |\r\n________\r\n", table);
        }

        [Fact]
        public void When_tabulate_called_with_length_of_header_strings_longer_than_value_strings_then_table_returned()
        {
            var headers = new string[]
            {
                "XXXHeader1",
                "YYYHeader2",
                "ZZZHeader3",
            };

            var values = new string[][]
            {
                new string [] { "value1A", "value1B", "value1C" },
                new string [] { "value2A", "value2B", "value2C" },
                new string [] { "value3A", "value3B", "value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
|--------------------------------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|--------------------------------|
|{values[1][0]}   |{values[1][1]}   |{values[1][2]}   |
|--------------------------------|
|{values[2][0]}   |{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_length_of_value_strings_longer_than_header_strings_then_table_returned()
        {
            var headers = new string[]
            {
                "Header1",
                "Header2",
                "Header3",
            };

            var values = new string[][]
            {
                new string [] { "XXXvalue1A", "XXXvalue1B", "XXXvalue1C" },
                new string [] { "YYYvalue2A", "YYYvalue2B", "YYYvalue2C" },
                new string [] { "ZZZvalue3A", "ZZZvalue3B", "ZZZvalue3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}   |{headers[1]}   |{headers[2]}   |
|--------------------------------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|--------------------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|--------------------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_no_headers_and_multiple_rows_then_table_returned()
        {
            var values = new string[][]
            {
                new string [] { "value1A", "value1B", "value1C" },
                new string [] { "value2A", "value2B", "value2C" },
                new string [] { "value3A", "value3B", "value3C" },
            };

            var expected =
@$"-------------------------
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-----------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-----------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_tablevalue_delegates_for_multiple_headers_and_multiple_rows_then_table_returned()
        {
            var headers = new TableValue[]
            {
                () => "Header1",
                () => "Header2",
                () => "Header3",
            };

            var values = new TableValue[][]
            {
                new TableValue [] { () => "value1A", () => "value1B", () => "value1C" },
                new TableValue [] { () => "value2A", () => "value2B", () => "value2C" },
                new TableValue [] { () => "value3A", () => "value3B", () => "value3C" },
            };

            var expected =
@$"-------------------------
|{headers[0]()}|{headers[1]()}|{headers[2]()}|
|-----------------------|
|{values[0][0]()}|{values[0][1]()}|{values[0][2]()}|
|-----------------------|
|{values[1][0]()}|{values[1][1]()}|{values[1][2]()}|
|-----------------------|
|{values[2][0]()}|{values[2][1]()}|{values[2][2]()}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_tablevalue_delegates_for_multiple_rows_then_table_returned()
        {
            var values = new TableValue[][]
            {
                new TableValue [] { () => "value1A", () => "value1B", () => "value1C" },
                new TableValue [] { () => "value2A", () => "value2B", () => "value2C" },
                new TableValue [] { () => "value3A", () => "value3B", () => "value3C" },
            };

            var expected =
@$"-------------------------
|{values[0][0]()}|{values[0][1]()}|{values[0][2]()}|
|-----------------------|
|{values[1][0]()}|{values[1][1]()}|{values[1][2]()}|
|-----------------------|
|{values[2][0]()}|{values[2][1]()}|{values[2][2]()}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_objects_for_multiple_headers_and_multiple_rows_then_table_returned()
        {
            var headers = new ToStringFake[]
            {
                new ("Header1"),
                new ("Header2"),
                new ("Header3"),
            };

            var values = new ToStringFake[][]
            {
                new ToStringFake [] { new ("value1A"), new ("value1B"), new ("value1C") },
                new ToStringFake [] { new ("value2A"), new ("value2B"), new ("value2C") },
                new ToStringFake [] { new ("value3A"), new ("value3B"), new ("value3C") },
            };

            var expected =
@$"-------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
|-----------------------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-----------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-----------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_objects_for_multiple_rows_then_table_returned()
        {
            var values = new ToStringFake[][]
            {
                new ToStringFake [] { new ("value1A"), new ("value1B"), new ("value1C") },
                new ToStringFake [] { new ("value2A"), new ("value2B"), new ("value2C") },
                new ToStringFake [] { new ("value3A"), new ("value3B"), new ("value3C") },
            };

            var expected =
@$"-------------------------
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-----------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-----------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_headers_longer_uniform_alignment_left_then_table_returned()
        {
            var headers = new string[]
            {
                "XXXHeader1",
                "YYYHeader2",
                "ZZZHeader3",
            };

            var values = new string[][]
            {
                new string [] { "value1A", "value1B", "value1C" },
                new string [] { "value2A", "value2B", "value2C" },
                new string [] { "value3A", "value3B", "value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
|--------------------------------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|--------------------------------|
|{values[1][0]}   |{values[1][1]}   |{values[1][2]}   |
|--------------------------------|
|{values[2][0]}   |{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignmentProvider = new UniformAlignmentProvider(CellAlignment.Left)
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_headers_longer_uniform_alignment_right_then_table_returned()
        {
            var headers = new string[]
            {
                "XXXHeader1",
                "YYYHeader2",
                "ZZZHeader3",
            };

            var values = new string[][]
            {
                new string [] { "value1A", "value1B", "value1C" },
                new string [] { "value2A", "value2B", "value2C" },
                new string [] { "value3A", "value3B", "value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
|--------------------------------|
|   {values[0][0]}|   {values[0][1]}|   {values[0][2]}|
|--------------------------------|
|   {values[1][0]}|   {values[1][1]}|   {values[1][2]}|
|--------------------------------|
|   {values[2][0]}|   {values[2][1]}|   {values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignmentProvider = new UniformAlignmentProvider(CellAlignment.Right)
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_values_longer_uniform_alignment_left_then_table_returned()
        {
            var headers = new string[]
            {
                "Header1",
                "Header2",
                "Header3",
            };

            var values = new string[][]
            {
                new string [] { "XXXvalue1A", "XXXvalue1B", "XXXvalue1C" },
                new string [] { "YYYvalue2A", "YYYvalue2B", "YYYvalue2C" },
                new string [] { "ZZZvalue3A", "ZZZvalue3B", "ZZZvalue3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}   |{headers[1]}   |{headers[2]}   |
|--------------------------------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|--------------------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|--------------------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignmentProvider = new UniformAlignmentProvider(CellAlignment.Left)
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_values_longer_uniform_alignment_right_then_table_returned()
        {
            var headers = new string[]
            {
                "Header1",
                "Header2",
                "Header3",
            };

            var values = new string[][]
            {
                new string [] { "XXXvalue1A", "XXXvalue1B", "XXXvalue1C" },
                new string [] { "YYYvalue2A", "YYYvalue2B", "YYYvalue2C" },
                new string [] { "ZZZvalue3A", "ZZZvalue3B", "ZZZvalue3C" },
            };

            var expected =
@$"----------------------------------
|   {headers[0]}|   {headers[1]}|   {headers[2]}|
|--------------------------------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|--------------------------------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|--------------------------------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignmentProvider = new UniformAlignmentProvider(CellAlignment.Right)
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_custom_joinery_then_table_returned()
        {
            var headers = new string[]
            {
                "Header1",
                "YYYHeader2",
                "Header3",
            };

            var values = new string[][]
            {
                new string [] { "XXXvalue1A", "value2A", "ZZZvalue3A" },
                new string [] { "XXXvalue1B", "value2B", "ZZZvalue3B" },
                new string [] { "XXXvalue1C", "value2C", "ZZZvalue3C" },
            };

            var expected =
@$"┌──────────┬──────────┬──────────┐
│{headers[0]}   │{headers[1]}│{headers[2]}   │
├──────────┼──────────┼──────────┤
│{values[0][0]}│   {values[0][1]}│{values[0][2]}│
├──────────┼──────────┼──────────┤
│{values[1][0]}│   {values[1][1]}│{values[1][2]}│
├──────────┼──────────┼──────────┤
│{values[2][0]}│   {values[2][1]}│{values[2][2]}│
└──────────┴──────────┴──────────┘
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignmentProvider = new UniformHeaderUniformValueAlignmentProvider(),
                RowSeparator = '─',
                ColumnSeparator = '│',
                TopLeftCorner = '┌',
                TopRightCorner = '┐',
                BottomLeftCorner = '└',
                BottomRightCorner = '┘',
                LeftEdgeJoint = '├',
                RightEdgeJoint = '┤',
                TopEdgeJoint = '┬',
                BottomEdgeJoint = '┴',
                MiddleJoint = '┼'
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }
    }
}
