namespace Cint.RobotCleaner.Client.Extensions;

public static class ClientServiceExtensions
{
    public static IServiceCollection AddClientDepedency(this IServiceCollection services)
    {
        services.AddSingleton<IConsoleCommandReader, ConsoleCommandReader>();
        services.AddSingleton<IConsoleIo, ConsoleIo>();
        return services;
    }
}