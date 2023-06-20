namespace MarketHub.Application.Services.Queries.Common.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindAllQueryHandler<TEntity> : IQueryHandler<FindAllQuery, List<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public FindAllQueryHandler(IRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<List<TEntity>> HandleAsync(FindAllQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetListAsync(cancellationToken);
    }
}