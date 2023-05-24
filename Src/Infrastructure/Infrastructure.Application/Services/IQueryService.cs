namespace Infrastructure.Application.Services;

using Domain.Entities;

public interface IQueryService<TEntity>
    where TEntity : Entity
{
    Task<TEntity?> FindByIdAsync(long id,
        CancellationToken cancellationToken = default);
}