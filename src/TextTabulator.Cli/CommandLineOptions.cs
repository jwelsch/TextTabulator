namespace TextTabulator.Cli
{
    public interface ICommandLineOptions
    {
        DataType AdapterType { get; }

        string InputPath { get; }

        string? OutputPath { get; }
    }

    public class CommandLineOptions : ICommandLineOptions
    {
        public DataType AdapterType { get; }

        public string InputPath { get; }

        public string? OutputPath { get; }

        public CommandLineOptions(DataType adapterType, string inputPath, string? outputPath = null)
        {
            AdapterType = adapterType;
            InputPath = inputPath;
            OutputPath = outputPath;
        }
    }
}
