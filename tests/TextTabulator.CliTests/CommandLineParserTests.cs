using TextTabulator.Cli;

namespace TextTabulator.CliTests
{
    public class CommandLineParserTests
    {
        [Fact]
        public void When_args_is_empty_then_throw_argumentexception()
        {
            var args = new string[0];

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_unknown_argument_then_throw_argumentexception()
        {
            var args = new string[]
            {
                "--this-is-an-unknown-argument"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_no_value_then_throw_argumentexception()
        {
            var args = new string[]
            {
                "--input-path"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_no_input_path_then_throw_argumentexception()
        {
            var args = new string[]
            {
                "--output-path", @"C:\Some\Path\table.txt"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_unknown_extension_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_csv_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_json_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.json";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_xml_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.xml";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Xml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_yaml_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.yaml";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Yaml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_yml_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.yml";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Yaml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_mixed_case_extension_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.jSoN";
            var args = new string[]
            {
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_csv_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", DataType.Csv.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_json_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", DataType.Json.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_xml_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", DataType.Xml.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Xml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_yaml_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", DataType.Yaml.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Yaml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_yml_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", "yml"
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Yaml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_txt_extension_and_data_type_mixed_case_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", "YaMl"
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Yaml, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_with_csv_extension_and_data_type_is_unknown_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--data-type", "TXT"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_output_path_with_no_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path",
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_output_path_with_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path", outputPath
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_output_path_with_value_and_data_type_with_no_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path", outputPath,
                "--data-type"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_txt_extension_and_output_path_with_value_and_data_type_with_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.txt";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path", outputPath,
                "--data-type", DataType.Json.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_valid_data_type_extension_and_output_path_with_value_and_data_type_with_different_data_type_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path", outputPath,
                "--data-type", DataType.Json.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_output_path_with_no_value_and_data_type_with_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--output-path",
                "--data-type", DataType.Json.ToString()
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_styling_with_no_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--styling"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_styling_with_no_value_and_input_path_with_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--styling",
                "--input-path", inputPath
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_styling_with_unknown_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--styling", "this-is-an-unknown-style"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_styling_with_unicode_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--styling", TableStyling.Unicode.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Unicode, result.Styling);
        }

        [Fact]
        public void When_args_has_input_path_with_value_and_styling_with_ascii_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--styling", TableStyling.Ascii.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
        }

        [Fact]
        public void When_args_has_output_path_with_value_and_data_type_with_value_and_styling_with_value_and_input_path_with_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.json";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--output-path", outputPath,
                "--data-type", DataType.Json.ToString(),
                "--styling", TableStyling.Unicode.ToString(),
                "--input-path", inputPath,
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
            Assert.Equal(TableStyling.Unicode, result.Styling);
        }

        [Fact]
        public void When_args_has_i_value_and_o_with_value_and_d_with_value_and_s_with_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.json";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "-i", inputPath,
                "-o", outputPath,
                "-d", DataType.Json.ToString(),
                "-s", TableStyling.Unicode.ToString(),
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
            Assert.Equal(TableStyling.Unicode, result.Styling);
        }

        [Fact]
        public void When_args_has_tab_length_with_positive_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var tabLength = 4;
            var args = new string[]
            {
                "--input-path", inputPath,
                "--tab-length", tabLength.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
            Assert.Equal(tabLength, result.TabLength);
        }

        [Fact]
        public void When_args_has_tab_length_with_zero_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var tabLength = 0;
            var args = new string[]
            {
                "--input-path", inputPath,
                "--tab-length", tabLength.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
            Assert.Equal(tabLength, result.TabLength);
        }

        [Fact]
        public void When_args_has_tab_length_with_negative_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var tabLength = -3;
            var args = new string[]
            {
                "--input-path", inputPath,
                "--tab-length", tabLength.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
            Assert.Equal(tabLength, result.TabLength);
        }

        [Fact]
        public void When_args_has_tab_length_with_no_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--tab-length"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_tab_length_with_invalid_value_then_throw_formatexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "--input-path", inputPath,
                "--tab-length", "invalid"
            };

            var sut = new CommandLineParser();

            Assert.Throws<FormatException>(() => sut.Parse(args));
        }

        [Fact]
        public void When_args_has_short_t_option_with_positive_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var tabLength = 8;
            var args = new string[]
            {
                "-i", inputPath,
                "-t", tabLength.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Csv, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Null(result.OutputPath);
            Assert.Equal(TableStyling.Ascii, result.Styling);
            Assert.Equal(tabLength, result.TabLength);
        }

        [Fact]
        public void When_args_has_short_t_option_with_no_value_then_throw_argumentexception()
        {
            var inputPath = @"C:\Some\Path\data.csv";
            var args = new string[]
            {
                "-i", inputPath,
                "-t"
            };

            var sut = new CommandLineParser();

            Assert.Throws<ArgumentException>(() => sut.Parse(args));
        }
    }
}
