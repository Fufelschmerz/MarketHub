namespace MarketHub.Application.Services.Queries.Common.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindByIdQueryHandler<TEntity> : IQueryHandler<FindByIdQuery, TEntity?>
    where TEntity : class, IEntity
{
    private readonly IDbRepository<TEntity> _dbRepository;

    public FindByIdQueryHandler(IDbRepository<TEntity> dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }
    
    public Task<TEntity?> HandleAsync(FindByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return _dbRepository.GetByIdAsync(query.Id,
            cancellationToken);
    }
}