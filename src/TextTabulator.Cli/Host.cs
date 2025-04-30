using Microsoft.Extensions.DependencyInjection;

namespace TextTabulator.Cli
{
    internal class Host
    {
        private readonly ICommandLineParser _commandLineParser;

        public Host(IServiceProvider serviceProvider)
        {
            _commandLineParser = serviceProvider.GetRequiredService<ICommandLineParser>();
        }

        public int Run(string[] args)
        {
            var commandLineOptions = _commandLineParser.Parse(args);

            return 0;
        }
    }
}
