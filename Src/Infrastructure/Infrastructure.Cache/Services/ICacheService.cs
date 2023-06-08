namespace Infrastructure.Cache.Services;

public interface ICacheService<T>
{
    Task AddAsync(string key,
        T value,
        TimeSpan? expiry = null);
    
    Task<T?> GetAsync(string key);

    Task DeleteAsync(string key);
}