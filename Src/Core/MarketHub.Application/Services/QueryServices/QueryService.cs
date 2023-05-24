namespace MarketHub.Application.Services.QueryServices;

using global::Infrastructure.Application.Services;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public abstract class QueryService<TEntity, TRepository> : IQueryService<TEntity>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    protected readonly TRepository _repository;

    protected QueryService(TRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public virtual Task<TEntity?> FindByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }
}