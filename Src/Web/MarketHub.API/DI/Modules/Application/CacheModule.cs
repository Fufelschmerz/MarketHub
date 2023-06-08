namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Autofac.ConfiguredModules;
using Infrastructure.Cache.Services;
using StackExchange.Redis;

internal sealed class CacheModule : ConfiguredModule
{
    protected override void Load(ContainerBuilder builder)
    {
        string redisHost = Configuration.GetValue<string>("Redis:Host")!;

        builder.Register(_ => ConnectionMultiplexer.Connect(redisHost))
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterGeneric(typeof(RedisCacheService<>))
            .As(typeof(ICacheService<>))
            .InstancePerLifetimeScope();
    }
}