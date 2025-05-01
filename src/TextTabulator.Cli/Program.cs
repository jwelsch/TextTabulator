using Microsoft.Extensions.DependencyInjection;
using TextTabulator.Cli;
using TextTabulator.Cli.Wraps;

#region Dependency Injection

public abstract class Module
{
    public void LoadModule(IServiceCollection services, object[]? parameters = null)
    {
        Load(services, parameters);
    }

    protected virtual void Load(IServiceCollection services, object[]? parameters = null)
    {
    }
}

public static class ServiceCollectionExtensions
{
    public static void RegisterModule<TModule>(this IServiceCollection services, object[]? parameters = null) where TModule : Module
    {
        var module = Activator.CreateInstance<TModule>();
        module.LoadModule(services, parameters);
    }
}

#endregion

internal class Program
{
    private static int Main(string[] args)
    {
        try
        {
            var sp = RegisterAppServices();

            var host = new Host(
                sp.GetRequiredService<IConsoleWrap>(),
                sp.GetRequiredService<IFileWrap>(),
                sp.GetRequiredService<IStreamWriterWrapFactory>(),
                sp.GetRequiredService<ICommandLineParser>()
            );

            return host.Run(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return -1;
    }

    private static IServiceProvider RegisterAppServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<IFileWrap, FileWrap>();
        services.AddTransient<IStreamWriterWrapFactory, StreamWriterWrapFactory>();
        services.AddTransient<IFileStreamWrapFactory, FileStreamWrapFactory>();
        services.AddTransient<IConsoleWrap, ConsoleWrap>();
        services.AddTransient<ICommandLineParser, CommandLineParser>();

        return services.BuildServiceProvider();
    }
}