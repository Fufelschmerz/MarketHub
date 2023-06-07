namespace MarketHub.API.DI.ConfigureServices;

using Microsoft.EntityFrameworkCore;
using Persistence;

internal static class ConfigureServiceEfCore
{
    internal static IServiceCollection ConfigureDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<DataContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}