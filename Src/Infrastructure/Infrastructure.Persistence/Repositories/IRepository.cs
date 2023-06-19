namespace Infrastructure.Persistence.Repositories;

using Domain.Entities;
using Specifications;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task AddAsync(TEntity entity,
        CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
    
    Task DeleteAsync(TEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(long id,
        CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}