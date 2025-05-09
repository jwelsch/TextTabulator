namespace TextTabulator.Cli
{
    public interface ICommandLineOptions
    {
        DataType AdapterType { get; }

        string InputPath { get; }

        string? OutputPath { get; }

        TableStyling Styling { get; }

        bool IncludeNonPrintableCharacters { get; }
    }

    public class CommandLineOptions : ICommandLineOptions
    {
        public DataType AdapterType { get; }

        public string InputPath { get; }

        public string? OutputPath { get; }
        public TableStyling Styling { get; }

        public bool IncludeNonPrintableCharacters { get; }

        public CommandLineOptions(DataType adapterType, string inputPath, string? outputPath = null, TableStyling styling = TableStyling.Ascii, bool includeNonPrintableCharacters = false)
        {
            AdapterType = adapterType;
            InputPath = inputPath;
            OutputPath = outputPath;
            Styling = styling;
            IncludeNonPrintableCharacters = includeNonPrintableCharacters;
        }
    }
}
