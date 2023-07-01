namespace MarketHub.Application.Services.Queries.Common.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindAllQueryHandler<TEntity> : IQueryHandler<FindAllQuery, List<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IDbRepository<TEntity> _dbRepository;

    public FindAllQueryHandler(IDbRepository<TEntity> dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }
    
    public async Task<List<TEntity>> HandleAsync(FindAllQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _dbRepository.GetListAsync(cancellationToken);
    }
}