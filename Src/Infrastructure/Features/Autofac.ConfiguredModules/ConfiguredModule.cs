namespace Autofac.ConfiguredModules;

using Microsoft.Extensions.Configuration;

public abstract class ConfiguredModule : Module
{
    protected IConfiguration Configuration { get; set; }
}