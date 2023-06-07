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
        JwtTokenOptions jwtTokenOptions = Configuration.GetSection("Jwt:Authentication:Parameters")
            .Get<JwtTokenOptions>()!;

        builder.RegisterType<JwtTokenBuilder>()
            .AsImplementedInterfaces()
            .WithParameter("jwtTokenOptions", jwtTokenOptions)
            .InstancePerDependency();

        builder.RegisterType<AuthenticationService>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}