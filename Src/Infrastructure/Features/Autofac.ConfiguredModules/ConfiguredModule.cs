namespace Autofac.ConfiguredModules;

using Microsoft.Extensions.Configuration;

public abstract class ConfiguredModule : Module
{
    public IConfiguration Configuration { get; set; }
}