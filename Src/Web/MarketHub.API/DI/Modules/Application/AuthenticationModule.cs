namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Autofac.ConfiguredModules;
using Infrastructure.Application.Authentication.Builders;
using Infrastructure.Application.Authentication.Options;
using MarketHub.Application.Services.AuthenticationServices;

internal sealed class AuthenticationModule : ConfiguredModule
{
    protected override void Load(ContainerBuilder builder)
    {
        JwtOptions jwtOptions = Configuration.GetSection("Jwt:Authentication:Parameters")
            .Get<JwtOptions>() ?? throw new ArgumentNullException(nameof(JwtOptions));

        builder.RegisterType<AccessBuilder>()
            .AsImplementedInterfaces()
            .WithParameter("jwtOptions", jwtOptions)
            .InstancePerDependency();

        builder.RegisterType<AuthenticationService>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}