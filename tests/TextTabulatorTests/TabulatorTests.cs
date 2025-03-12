using NSubstitute;
using TextTabulator;
using TextTabulator.Adapter;

namespace TextTabulatorTests
{
    /// <summary>
    /// Note for these tests, the NewLine should be set since the new lines in
    /// TabulatorTests.cs can be controlled, but the new lines in
    /// Environment.NewLine is dependent on what type of system runs the tests.
    /// </summary>
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

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\n" });

            Assert.Equal($"--------\n|{headers[0]}|\n|------|\n|{values[0][0]} |\n--------\n", table);
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
                Styling = new AsciiTableStyling
                {
                    ColumnSeparator = '#',
                    LeftEdge = '#',
                    RightEdge = '#',
                },
                NewLine = "\r",
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal($"--------\r#{headers[0]}#\r|------|\r#{values[0][0]} #\r--------\r", table);
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
                Styling = new AsciiTableStyling
                {
                    ColumnLeftPadding = "#"
                },
                NewLine = "\r\n",
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
                Styling = new AsciiTableStyling
                {
                    ColumnLeftPadding = "__"
                },
                NewLine = "\r\n",
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
                Styling = new AsciiTableStyling
                {
                    ColumnRightPadding = "#"
                },
                NewLine = "\r\n",
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
                Styling = new AsciiTableStyling
                {
                    ColumnRightPadding = "__"
                },
                NewLine = "\r\n",
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
                Styling = new AsciiTableStyling
                {
                    ColumnLeftPadding = "@",
                    ColumnRightPadding = "#"
                },
                NewLine = "\r\n",
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
                Styling = new AsciiTableStyling
                {
                    ColumnLeftPadding = "@@@",
                    ColumnRightPadding = "###"
                },
                NewLine = "\r\n",
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

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

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
|-------+-------+-------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-------+-------+-------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-------+-------+-------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

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
                Styling = new AsciiTableStyling
                {
                    ValueRowSeparator = '_',
                    ValueLeftEdgeJoint = '_',
                    ValueRightEdgeJoint = '_',
                    HeaderRowSeparator = '_',
                    HeaderLeftEdgeJoint = '_',
                    HeaderRightEdgeJoint = '_',
                    TopLeftCorner = '_',
                    TopRightCorner = '_',
                    TopEdge = '_',
                    BottomLeftCorner = '_',
                    BottomRightCorner = '_',
                    BottomEdge = '_',
                },
                NewLine = "\r\n",
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
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}   |{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}   |{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

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
|----------+----------+----------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|----------+----------+----------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

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
|-------+-------+-------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-------+-------+-------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_tablevalue_delegates_for_multiple_headers_and_multiple_rows_then_table_returned()
        {
            var headers = new CellValue[]
            {
                () => "Header1",
                () => "Header2",
                () => "Header3",
            };

            var values = new CellValue[][]
            {
                new CellValue [] { () => "value1A", () => "value1B", () => "value1C" },
                new CellValue [] { () => "value2A", () => "value2B", () => "value2C" },
                new CellValue [] { () => "value3A", () => "value3B", () => "value3C" },
            };

            var expected =
@$"-------------------------
|{headers[0]()}|{headers[1]()}|{headers[2]()}|
|-------+-------+-------|
|{values[0][0]()}|{values[0][1]()}|{values[0][2]()}|
|-------+-------+-------|
|{values[1][0]()}|{values[1][1]()}|{values[1][2]()}|
|-------+-------+-------|
|{values[2][0]()}|{values[2][1]()}|{values[2][2]()}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_tablevalue_delegates_for_multiple_rows_then_table_returned()
        {
            var values = new CellValue[][]
            {
                new CellValue [] { () => "value1A", () => "value1B", () => "value1C" },
                new CellValue [] { () => "value2A", () => "value2B", () => "value2C" },
                new CellValue [] { () => "value3A", () => "value3B", () => "value3C" },
            };

            var expected =
@$"-------------------------
|{values[0][0]()}|{values[0][1]()}|{values[0][2]()}|
|-------+-------+-------|
|{values[1][0]()}|{values[1][1]()}|{values[1][2]()}|
|-------+-------+-------|
|{values[2][0]()}|{values[2][1]()}|{values[2][2]()}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values, new TabulatorOptions { NewLine = "\r\n" });

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
|-------+-------+-------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|-------+-------+-------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-------+-------+-------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

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
|-------+-------+-------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|-------+-------+-------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
-------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(values, new TabulatorOptions { NewLine = "\r\n" });

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
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}   |{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}   |{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.Left),
                NewLine = "\r\n",
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
|----------+----------+----------|
|   {values[0][0]}|   {values[0][1]}|   {values[0][2]}|
|----------+----------+----------|
|   {values[1][0]}|   {values[1][1]}|   {values[1][2]}|
|----------+----------+----------|
|   {values[2][0]}|   {values[2][1]}|   {values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.Right),
                NewLine = "\r\n",
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
|----------+----------+----------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|----------+----------+----------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.Left),
                NewLine = "\r\n",
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
|----------+----------+----------|
|{values[0][0]}|{values[0][1]}|{values[0][2]}|
|----------+----------+----------|
|{values[1][0]}|{values[1][1]}|{values[1][2]}|
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}|{values[2][2]}|
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.Right),
                NewLine = "\r\n",
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
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"╔══════════╤══════════╤══════════╗
║{headers[0]}   │{headers[1]}   │{headers[2]}║
╠══════════╪══════════╪══════════╣
║{values[0][0]}   │   {values[0][1]}│   {values[0][2]}║
╟──────────┼──────────┼──────────╢
║{values[1][0]}   │{values[1][1]}│   {values[1][2]}║
╟──────────┼──────────┼──────────╢
║{values[2][0]}│   {values[2][1]}│   {values[2][2]}║
╚══════════╧══════════╧══════════╝
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }),
                Styling = new UnicodeTableStyling(),
                NewLine = "\r\n",
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_center_left_bias_then_table_returned()
        {
            var headers = new string[]
            {
                "Header",
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|  {headers[0]}  | {headers[1]}  |{headers[2]}|
|----------+----------+----------|
| {values[0][0]}  | {values[0][1]}  | {values[0][2]}  |
|----------+----------+----------|
| {values[1][0]}  |{values[1][1]}| {values[1][2]}  |
|----------+----------+----------|
|{values[2][0]}| {values[2][1]}  | {values[2][2]}  |
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.CenterLeftBias),
                NewLine = "\r\n",
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_center_right_bias_then_table_returned()
        {
            var headers = new string[]
            {
                "Header",
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|  {headers[0]}  |  {headers[1]} |{headers[2]}|
|----------+----------+----------|
|  {values[0][0]} |  {values[0][1]} |  {values[0][2]} |
|----------+----------+----------|
|  {values[1][0]} |{values[1][1]}|  {values[1][2]} |
|----------+----------+----------|
|{values[2][0]}|  {values[2][1]} |  {values[2][2]} |
----------------------------------
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformAlignmentProvider(CellAlignment.CenterRightBias),
                NewLine = "\r\n",
            };

            var table = sut.Tabulate(headers, values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_no_headers_then_table_returned()
        {
            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"╔══════════╤══════════╤═══════╗
║{values[0][0]}   │   {values[0][1]}│{values[0][2]}║
╟──────────┼──────────┼───────╢
║{values[1][0]}   │{values[1][1]}│{values[1][2]}║
╟──────────┼──────────┼───────╢
║{values[2][0]}│   {values[2][1]}│{values[2][2]}║
╚══════════╧══════════╧═══════╝
";

            var sut = new Tabulator();
            var options = new TabulatorOptions
            {
                CellAlignment = new UniformHeaderUniformColumnAlignmentProvider(new CellAlignment[] { CellAlignment.Left, CellAlignment.Right }, CellAlignment.CenterLeftBias),
                Styling = new UnicodeTableStyling(),
                NewLine = "\r\n",
            };

            var table = sut.Tabulate(values, options);

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_adapter_used_with_headers_and_values_then_expected_table_is_returned()
        {
            var headers = new string[]
            {
                "Header",
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}    |{headers[1]}   |{headers[2]}|
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}|{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var adapter = Substitute.For<ITabulatorAdapter>();

            adapter.GetHeaderStrings().Returns(headers);
            adapter.GetValueStrings().Returns(values);

            var sut = new Tabulator();

            var table = sut.Tabulate(adapter, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_adapter_used_with_values_then_expected_table_is_returned()
        {
            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"-------------------------------
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}|
|----------+----------+-------|
|{values[1][0]}   |{values[1][1]}|{values[1][2]}|
|----------+----------+-------|
|{values[2][0]}|{values[2][1]}   |{values[2][2]}|
-------------------------------
";

            var adapter = Substitute.For<ITabulatorAdapter>();

            adapter.GetHeaderStrings().Returns((IEnumerable<string>?)null);
            adapter.GetValueStrings().Returns(values);

            var sut = new Tabulator();

            var table = sut.Tabulate(adapter, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_no_headers_and_no_rows_then_empty_string_is_returned()
        {
            var headers = new string[0];
            var values = new string[0][];

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values);

            Assert.Equal(string.Empty, table);
        }

        [Fact]
        public void When_tabulate_called_with_only_headers_then_data_is_returned()
        {
            var headers = new string[]
            {
                "Header",
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[0][];

            var expected =
@$"---------------------------
|{headers[0]}|{headers[1]}|{headers[2]}|
---------------------------
";

            var sut = new Tabulator();

            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_empty_string_in_header_then_table_returned()
        {
            var headers = new string[]
            {
                "Header",
                string.Empty,
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", "Value3A" },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}    |{headers[1]}          |{headers[2]}|
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}   |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}|{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();
            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_empty_string_in_values_then_table_returned()
        {
            var headers = new string[]
            {
                "Header",
                "Header2",
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", string.Empty },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}    |{headers[1]}   |{headers[2]}|
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}          |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}|{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();
            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }

        [Fact]
        public void When_tabulate_called_with_empty_string_in_headers_and_values_then_table_returned()
        {
            var headers = new string[]
            {
                "Header",
                string.Empty,
                "ZZZHeader3"
            };

            var values = new string[][]
            {
                new string[] { "Value1A", "Value2A", string.Empty },
                new string[] { "Value1B", "YYYValue2B", "Value3B" },
                new string[] { "XXXValue1C", "Value2C", "Value3C" },
            };

            var expected =
@$"----------------------------------
|{headers[0]}    |{headers[1]}          |{headers[2]}|
|----------+----------+----------|
|{values[0][0]}   |{values[0][1]}   |{values[0][2]}          |
|----------+----------+----------|
|{values[1][0]}   |{values[1][1]}|{values[1][2]}   |
|----------+----------+----------|
|{values[2][0]}|{values[2][1]}   |{values[2][2]}   |
----------------------------------
";

            var sut = new Tabulator();
            var table = sut.Tabulate(headers, values, new TabulatorOptions { NewLine = "\r\n" });

            Assert.Equal(expected, table);
        }
    }
}
