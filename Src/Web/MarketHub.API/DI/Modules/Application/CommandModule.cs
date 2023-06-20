namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Infrastructure.Application.Services.Commands;
using Infrastructure.Application.Services.Commands.Dispatchers;
using MarketHub.Application.Services.Commands.Handlers;

internal sealed class CommandModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterGeneric(typeof(CreateEntityCommandHandler<>))
            .As(typeof(ICommandHandler<>))
            .InstancePerDependency();
        
        builder
            .RegisterGeneric(typeof(UpdateEntityCommandHandler<>))
            .As(typeof(ICommandHandler<>))
            .InstancePerLifetimeScope();

        builder
            .RegisterGeneric(typeof(DeleteEntityCommandHandler<>))
            .As(typeof(ICommandHandler<>))
            .InstancePerLifetimeScope();
        
        builder
            .RegisterType<CommandDispatcher>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}