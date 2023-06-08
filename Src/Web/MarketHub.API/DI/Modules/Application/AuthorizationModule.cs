namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using MarketHub.Application.Services.AuthorizationServices;

internal sealed class AuthorizationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AuthorizationService>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}