namespace RobotCleaner.Application.Extentions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(IRobotCleanerRoot));
            });
        return services;
    }
}