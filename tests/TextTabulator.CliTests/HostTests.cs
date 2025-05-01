using NSubstitute;
using TextTabulator.Cli;
using TextTabulator.Cli.Wraps;
using TextTabulator.Testing;

namespace TextTabulator.CliTests
{
    public class HostTests
    {
        [Fact]
        public void When_input_path_is_not_file_on_system_then_throw_filenotfoundexception()
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

            consoleWrap.Received(1);
            Assert.NotEqual(0, result);
        }
    }
}
