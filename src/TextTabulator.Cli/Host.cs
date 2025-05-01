using CsvHelper;
using System.Globalization;
using TextTabulator.Adapters;
using TextTabulator.Adapters.CsvHelper;
using TextTabulator.Adapters.Json;
using TextTabulator.Adapters.Xml;
using TextTabulator.Adapters.YamlDotNet;
using TextTabulator.Cli.Wraps;
using YamlDotNet.Core;

namespace TextTabulator.Cli
{
    public class Host
    {
        private readonly IConsoleWrap _consoleWrap;
        private readonly IFileWrap _fileWrap;
        private readonly IStreamWriterWrapFactory _streamWriterWrapFactory;
        private readonly ICommandLineParser _commandLineParser;

        public Host(IConsoleWrap consoleWrap, IFileWrap fileWrap, IStreamWriterWrapFactory streamWriterWrapFactory, ICommandLineParser commandLineParser)
        {
            _consoleWrap = consoleWrap;
            _fileWrap = fileWrap;
            _streamWriterWrapFactory = streamWriterWrapFactory;
            _commandLineParser = commandLineParser;
        }

        public int Run(string[] args)
        {
            var commandLineOptions = _commandLineParser.Parse(args);

            if (!_fileWrap.Exists(commandLineOptions.InputPath))
            {
                _consoleWrap.WriteLine($"The input file was not found at the specified path: '{commandLineOptions.InputPath}'");
                return 2; // ERROR_FILE_NOT_FOUND
            }

            TextReader? textReader = null;
            FileStream? inFileStream = null;
            CsvReader? csvReader = null;
            IStreamWriterWrap? outStreamWriter = null;
            TableCallback? callback = null;

            ITabulatorAdapter CreateCsvAdapter()
            {
                textReader = new StreamReader(commandLineOptions.InputPath);
                csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
                return new CsvHelperTabulatorAdapter(csvReader);
            }

            ITabulatorAdapter CreateJsonAdapter()
            {
                inFileStream = new FileStream(commandLineOptions.InputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return new JsonTabulatorAdapter(inFileStream);
            }

            ITabulatorAdapter CreateXmlAdapter()
            {
                inFileStream = new FileStream(commandLineOptions.InputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return new XmlTabulatorAdapter(inFileStream);
            }

            ITabulatorAdapter CreateYamlAdapter()
            {
                textReader = new StreamReader(commandLineOptions.InputPath);
                var parser = new Parser(textReader);
                return new YamlDotNetTabulatorAdapter(parser);
            }

            try
            {
                var adapter = commandLineOptions.AdapterType switch
                {
                    DataType.Csv => CreateCsvAdapter(),
                    DataType.Json => CreateJsonAdapter(),
                    DataType.Xml => CreateXmlAdapter(),
                    DataType.Yaml => CreateYamlAdapter(),
                    _ => throw new InvalidOperationException($"Unknown {nameof(DataType)} value: '{commandLineOptions.AdapterType}'.")
                };

                if (string.IsNullOrEmpty(commandLineOptions.OutputPath))
                {
                    callback = t => _consoleWrap.Write(t);
                }
                else
                {
                    outStreamWriter = _streamWriterWrapFactory.Create(commandLineOptions.OutputPath);
                    callback = t => outStreamWriter.Write(t);

                    _consoleWrap.WriteLine($"Writing table to file: {commandLineOptions.OutputPath}");
                }

                var options = new TabulatorOptions
                {
                    Styling = commandLineOptions.Styling == TableStyling.Ascii ? new AsciiTableStyling() : new UnicodeTableStyling()
                };

                var tabulator = new Tabulator();

                tabulator.Tabulate(adapter, callback, options);
            }
            finally
            {
                textReader?.Dispose();
                inFileStream?.Dispose();
                csvReader?.Dispose();

                if (outStreamWriter != null)
                {
                    outStreamWriter.Dispose();
                    _consoleWrap.WriteLine($"File write complete.");
                }
            }

            return 0;
        }
    }
}
