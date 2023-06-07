namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Infrastructure.Application.Services;
using MarketHub.Application;

internal sealed class QueryServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
            .AsClosedTypesOf(typeof(IQueryService<>))
            .InstancePerLifetimeScope();
    }
}