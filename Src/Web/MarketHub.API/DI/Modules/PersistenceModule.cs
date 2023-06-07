namespace MarketHub.API.DI.Modules;

using Autofac;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Specifications.Evaluator.Specification;
using Persistence;
using Persistence.UnitOfWorks;

internal sealed class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(PersistenceAssemblyMarker).Assembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<EfCoreUnitOfWork>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<SpecificationEvaluator>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}