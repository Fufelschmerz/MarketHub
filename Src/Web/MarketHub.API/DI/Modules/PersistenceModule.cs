namespace MarketHub.API.DI.Modules;

using Autofac;
using Domain.Entities.Users;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Specifications.Evaluator.Specification;
using Persistence.Repositories;
using Persistence.Repositories.Users;
using Persistence.UnitOfWorks;

internal sealed class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserRepository>()
            .As<IRepository<User>>()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<EfCoreUnitOfWork>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<SpecificationEvaluator>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}