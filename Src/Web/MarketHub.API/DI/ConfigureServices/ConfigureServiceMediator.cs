namespace MarketHub.API.DI.ConfigureServices;

using Application;
using Controllers;

internal static class ConfigureServiceMediator
{
    internal static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(opt =>
            opt.RegisterServicesFromAssemblies(typeof(ApplicationAssemblyMarker).Assembly,
                typeof(MarketHubApiController).Assembly));
    }
}