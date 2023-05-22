namespace Autofac.ConfiguredModules.Extensions;

using System.Reflection;
using Core;
using Microsoft.Extensions.Configuration;

public static class ConfiguredModulesContainerBuilderExtensions
{
    public static ContainerBuilder RegisterConfiguredModulesFromAssemblyContaining<TType>(
        this ContainerBuilder containerBuilder,
        IConfiguration configuration)
    {
        return RegisterConfiguredModulesFromAssembly(
            containerBuilder,
            typeof(TType).Assembly,
            configuration);
    }

    public static ContainerBuilder RegisterConfiguredModulesFromCurrentAssembly(this ContainerBuilder containerBuilder,
        IConfiguration configuration)
    {
        return RegisterConfiguredModulesFromAssembly(
            containerBuilder,
            Assembly.GetCallingAssembly(),
            configuration);
    }

    private static ContainerBuilder RegisterConfiguredModulesFromAssembly(this ContainerBuilder containerBuilder,
        Assembly assembly,
        IConfiguration configuration)
    {
        if (containerBuilder is null)
            throw new ArgumentNullException(nameof(containerBuilder));

        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        ContainerBuilder metaContainerBuilder = new();

        metaContainerBuilder
            .RegisterInstance(configuration);

        metaContainerBuilder
            .RegisterAssemblyTypes(assembly)
            .AssignableTo<IModule>()
            .As<IModule>()
            .PropertiesAutowired();

        using IContainer metaContainer = metaContainerBuilder.Build();

        foreach (IModule module in metaContainer.Resolve<IEnumerable<IModule>>())
            containerBuilder.RegisterModule(module);

        return containerBuilder;
    }
}