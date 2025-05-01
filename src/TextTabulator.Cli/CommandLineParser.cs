namespace TextTabulator.Cli
{
    public interface ICommandLineParser
    {
        ICommandLineOptions Parse(string[] args);
    }

    public class CommandLineParser : ICommandLineParser
    {
        public ICommandLineOptions Parse(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException($"No arguments specified.", nameof(args));
            }

            string? inputPath = null;
            string? outputPath = null;
            var dataType = DataType.Unknown;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("--input-path", StringComparison.OrdinalIgnoreCase) || args[i].Equals("-i", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        throw new ArgumentException($"No value for input path was found.", nameof(args));
                    }

                    inputPath = args[++i];
                }
                else if (args[i].Equals("--output-path", StringComparison.OrdinalIgnoreCase) || args[i].Equals("-o", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        throw new ArgumentException($"No value for output path was found.", nameof(args));
                    }

                    outputPath = args[++i];
                }
                else if (args[i].Equals("--data-type", StringComparison.OrdinalIgnoreCase) || args[i].Equals("-d", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        throw new ArgumentException($"No value for data type was found.", nameof(args));
                    }

                    if (!TryMatchDataType(args[i + 1], out dataType))
                    {
                        var types = Enum.GetNames<DataType>();
                        throw new ArgumentException($"Unknown data type value '{args[i + 1]}'. Must be one of: {string.Join(',', types)}.", nameof(args));
                    }

                    i++;
                }
                else
                {
                    throw new ArgumentException($"Unknown command line argument '{args[i]}' found.", nameof(args));
                }
            }

            if (string.IsNullOrEmpty(inputPath))
            {
                throw new ArgumentException($"Required argument \"--input-path\" not found.", nameof(args));
            }

            if (dataType == DataType.Unknown)
            {
                var extension = Path.GetExtension(inputPath);

                if (!TryMatchDataType(extension, out dataType))
                {
                    throw new ArgumentException($"Data type could not be determined. Specify a file with a known extension or use the \"--data-type\" argument.", nameof(args));
                }
            }

            return new CommandLineOptions(dataType, inputPath, outputPath);
        }

        private static bool TryMatchDataType(string input, out DataType dataType)
        {
            dataType = DataType.Unknown;

            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var format = input[0] == '.' ? input[1..] : input;

            if (format.Equals("yml", StringComparison.OrdinalIgnoreCase))
            {
                dataType = DataType.Yaml;
                return true;
            }

            return Enum.TryParse(format, true, out dataType);
        }
    }
}
