namespace Cint.RobotCleaner.Application.Extentions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}