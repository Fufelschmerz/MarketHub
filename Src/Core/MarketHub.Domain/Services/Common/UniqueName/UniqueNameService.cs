namespace MarketHub.Domain.Services.Common.UniqueName;

using Abstractions;

public abstract class UniqueNameService<T>
    where T : IHasUniqueName
{ 
    protected internal abstract Task<T?> FindWithSameNameAsync(string name,
        CancellationToken cancellationToken = default);
}