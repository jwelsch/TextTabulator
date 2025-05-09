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
            if (args.Length == 0)
            {
                _consoleWrap.WriteLine(HelpMessage());
                return 0;
            }

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
                    Styling = commandLineOptions.Styling == TableStyling.Ascii ? new AsciiTableStyling() : new UnicodeTableStyling(),
                    IncludeNonPrintableCharacters = commandLineOptions.IncludeNonPrintableCharacters,
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

        private static string HelpMessage()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly() ?? throw new Exception($"Could not get assembly.");
            var asmName = asm.GetName();
            var fileName = Path.GetFileNameWithoutExtension(asm.Location);

            return
$"""
{fileName}
version: {asmName.Version}

Project:
  https://github.com/jwelsch/TextTabulator/blob/main/src/TextTabulator

Licensed under the MIT License:
  https://github.com/jwelsch/TextTabulator/blob/main/src/TextTabulator/LICENSE

More information can be found in the README:
  https://github.com/jwelsch/TextTabulator/blob/main/src/TextTabulator/README.md

Command Line Arguments
----------------------

--data-type, -d

This argument is optional. This specifies the format of the data in the file specified by `--input-path`. The type of data should be the next argument on the command line. If this argument is not specified, the data format will be inferred from the file extension of `--input-path`. If this is specified, it will take precedence over the file extension.

--input-path, -i

This argument is required. This specifies the file that will be read from, and whose contents will be put into a table. The file path should be the next argument on the command line.

--output-path, -o

This argument is optional. This specifies what file path to write the table to. The file path should be the next argument on the command line. The table will be encoded as UTF-8 characters. If this argument is not included, the table will be written to the console.

--styling, -s

This argument is optional. This specifies which characters set to use to create the table. The value should be the next argument on the command line. If this argument is not included, the table will be created using ASCII characters.
""";
        }
    }
}
