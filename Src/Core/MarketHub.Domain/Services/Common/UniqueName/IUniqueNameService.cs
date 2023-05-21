namespace MarketHub.Domain.Services.Common.UniqueName;

using Abstractions;

public interface IUniqueNameService<T>
    where T : IHasUniqueName
{
    Task<T?> FindWithSameNameAsync(string newName,
        CancellationToken cancellationToken = default);
}