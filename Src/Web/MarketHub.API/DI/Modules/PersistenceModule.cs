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
        builder.RegisterType<UserDbRepository>()
            .As<IDbRepository<User>>()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(DbRepository<>))
            .As(typeof(IDbRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<EfCoreUnitOfWork>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<SpecificationEvaluator>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}