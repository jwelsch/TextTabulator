using NSubstitute;
using TextTabulator.Cli;
using TextTabulator.Cli.Wraps;
using TextTabulator.Testing;

namespace TextTabulator.CliTests
{
    public class HostTests
    {
        [Fact]
        public void When_input_path_is_not_file_on_system_then_write_error_to_console()
        {
            var args = new string[0];

            var autoSub = new AutoSubstitute();

            var consoleWrap = autoSub.Resolve<IConsoleWrap>();

            var fileWrap = autoSub.Resolve<IFileWrap>();
            fileWrap.Exists(Arg.Any<string>()).Returns(false);

            var commandLineOptions = autoSub.Resolve<ICommandLineOptions>();

            var commandLineParser = autoSub.Resolve<ICommandLineParser>();
            commandLineParser.Parse(Arg.Any<string[]>()).Returns(commandLineOptions);

            var sut = autoSub.Resolve<Host>();

            var result = sut.Run(args);

            Assert.NotEqual(0, result);
            consoleWrap.Received(1);
        }

        [Fact]
        public void When_csv_input_and_console_output_then_write_table_to_console()
        {
            var args = new string[]
            {
                "--input-path", "../../../../../data/dinosaurs.csv"
            };

            var autoSub = new AutoSubstitute();

            var consoleWrap = autoSub.Resolve<IConsoleWrap>();

            var fileWrap = autoSub.Resolve<IFileWrap>();
            fileWrap.Exists(Arg.Any<string>()).Returns(true);

            var commandLineOptions = new CommandLineOptions(DataType.Csv, args[1]);

            var commandLineParser = autoSub.Resolve<ICommandLineParser>();
            commandLineParser.Parse(Arg.Any<string[]>()).Returns(commandLineOptions);

            var sut = autoSub.Resolve<Host>();

            var result = sut.Run(args);

            Assert.Equal(0, result);
            Assert.True(consoleWrap.ReceivedCalls().Any());
        }

        [Fact]
        public void When_csv_input_and_file_output_then_write_table_to_file()
        {
            var args = new string[]
            {
                "--input-path", "../../../../../data/dinosaurs.csv",
                "--output-path", "../../../../../data/table.txt"
            };

            var autoSub = new AutoSubstitute();

            var consoleWrap = autoSub.Resolve<IConsoleWrap>();

            var fileWrap = autoSub.Resolve<IFileWrap>();
            fileWrap.Exists(Arg.Any<string>()).Returns(true);

            var streamWriterWrapFactory = autoSub.Resolve<IStreamWriterWrapFactory>();

            var commandLineOptions = new CommandLineOptions(DataType.Csv, args[1], args[3]);

            var commandLineParser = autoSub.Resolve<ICommandLineParser>();
            commandLineParser.Parse(Arg.Any<string[]>()).Returns(commandLineOptions);

            var sut = autoSub.Resolve<Host>();

            var result = sut.Run(args);

            Assert.Equal(0, result);
            Assert.True(consoleWrap.ReceivedCalls().Any());
            Assert.True(streamWriterWrapFactory.ReceivedCalls().Any());
        }
    }
}
