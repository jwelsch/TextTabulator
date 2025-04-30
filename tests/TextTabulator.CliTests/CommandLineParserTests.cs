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
        public void When_args_has_output_path_with_value_and_data_type_with_value_input_path_with_value_and_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.json";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "--output-path", outputPath,
                "--data-type", DataType.Json.ToString(),
                "--input-path", inputPath,
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
        }

        [Fact]
        public void When_args_has_i_value_and_o_with_value_and_d_with_value_then_return_commandlineoptions()
        {
            var inputPath = @"C:\Some\Path\data.json";
            var outputPath = @"C:\Some\Path\table.txt";
            var args = new string[]
            {
                "-i", inputPath,
                "-o", outputPath,
                "-d", DataType.Json.ToString()
            };

            var sut = new CommandLineParser();

            var result = sut.Parse(args);

            Assert.Equal(DataType.Json, result.AdapterType);
            Assert.Equal(inputPath, result.InputPath);
            Assert.Equal(outputPath, result.OutputPath);
        }
    }
}
