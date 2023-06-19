namespace MarketHub.Persistence.Repositories;

using Infrastructure.Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Evaluator.Specification;
using Microsoft.EntityFrameworkCore;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DataContext _dataContext;
    private readonly ISpecificationEvaluator _specificationEvaluator;
    
    protected readonly DbSet<TEntity> _entities;

    public Repository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        _specificationEvaluator =
            specificationEvaluator ?? throw new ArgumentNullException(nameof(specificationEvaluator));

        _entities = dataContext.Set<TEntity>();
    }

    public Task AddAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Add(entity);
        
        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        _entities.AddRange(entities);

        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Update(entity);

        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        _entities.UpdateRange(entities);

        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);

        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        _entities.RemoveRange(entities);

        return _dataContext.SaveChangesAsync(cancellationToken);
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }


    public virtual Task<int> CountAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(specification).CountAsync(cancellationToken);
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _entities.CountAsync(cancellationToken);
    }

    public virtual Task<bool> AnyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(specification).AnyAsync(cancellationToken);
    }

    public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return _entities.AnyAsync(cancellationToken);
    }

    public virtual Task<TEntity?> GetByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        return _entities.SingleOrDefaultAsync(x => x.Id == id,
            cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return _specificationEvaluator.BuildQuery(_entities.AsQueryable(),
            specification);
    }
}