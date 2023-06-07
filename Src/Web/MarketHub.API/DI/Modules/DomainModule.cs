namespace MarketHub.API.DI.Modules;

using Autofac;
using Domain;
using Infrastructure.Domain.Services;

internal sealed class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(DomainAssemblyMarker).Assembly)
            .AssignableTo<IDomainService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}