namespace MarketHub.API.DI.ConfigureServices;

using FluentValidation;
using FluentValidation.AspNetCore;
using Application;

internal static class ConfigureServiceFluentValidation
{
    internal static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
    {
        return services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
    }
}