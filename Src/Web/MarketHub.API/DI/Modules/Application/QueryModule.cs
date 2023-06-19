namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Infrastructure.Application.Services.Queries;
using Infrastructure.Application.Services.Queries.Dispatchers;
using MarketHub.Application;
using MarketHub.Application.Services.Queries.Common.Handlers;

public class QueryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterGeneric(typeof(FindByIdQueryHandler<>))
            .As(typeof(IQueryHandler<,>))
            .InstancePerDependency();
        
        builder
            .RegisterAssemblyTypes(typeof(ApplicationAssemblyMarker).Assembly)
            .AsClosedTypesOf(typeof(IQueryHandler<,>))
            .InstancePerDependency();

        builder
            .RegisterType<QueryDispatcher>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}