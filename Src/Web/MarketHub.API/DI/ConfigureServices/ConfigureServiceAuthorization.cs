namespace MarketHub.API.DI.ConfigureServices;

using Application.Infrastructure.Identity.Authorization.Admin.Handlers;
using Application.Infrastructure.Identity.Authorization.Admin.Requirements;
using Application.Infrastructure.Identity.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

internal static class ConfigureServiceAuthorization
{
    internal static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
        
        services.AddAuthorization(opt =>
        {
            opt.FallbackPolicy = DefaultPolicy.Create(JwtBearerDefaults.AuthenticationScheme);

            opt.AddPolicy(Policy.Admin,
                policy => policy.Requirements.Add(new AdminRequirement()));
        });

        return services;
    }
}