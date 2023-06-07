namespace MarketHub.API.DI.ConfigureServices;

using Application;

internal static class ConfigureServiceAutoMapper
{
    internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(ApplicationAssemblyMarker));
    }
}