using Microsoft.Extensions.DependencyInjection;
using TextTabulator.Cli;

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
        var sp = RegisterAppServices();

        // Not great to pass in the DI container, but meh.
        var host = new TextTabulator.Cli.Host(sp);

        return host.Run(args);
    }

    private static IServiceProvider RegisterAppServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<ICommandLineParser, CommandLineParser>();

        return services.BuildServiceProvider();
    }
}