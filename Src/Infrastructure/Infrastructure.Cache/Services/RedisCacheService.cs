namespace Infrastructure.Cache.Services;

using System.Text.Json;
using StackExchange.Redis;

public sealed class RedisCacheService<T> : ICacheService<T>
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
    }
    
    public async Task AddAsync(string key,
        T value,
        TimeSpan? expiry = null)
    {
        IDatabase database = _connectionMultiplexer.GetDatabase();

        await database.StringSetAsync(key,
            new RedisValue(JsonSerializer.Serialize(value)),
            expiry);
    }

    public async Task<T?> GetAsync(string key)
    {
        IDatabase database = _connectionMultiplexer.GetDatabase();

        string? value =  await database.StringGetAsync(key);

        return value is not null ? JsonSerializer.Deserialize<T>(value) : default;
    }

    public Task DeleteAsync(string key)
    {
        IDatabase database = _connectionMultiplexer.GetDatabase();

        return database.KeyDeleteAsync(key);
    }
}