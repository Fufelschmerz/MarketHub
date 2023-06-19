namespace MarketHub.Application.Services.Queries.Common.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindByIdQueryHandler<TEntity> : IQueryHandler<FindByIdQuery, TEntity?>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public FindByIdQueryHandler(IRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Task<TEntity?> HandleAsync(FindByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(query.Id,
            cancellationToken);
    }
}