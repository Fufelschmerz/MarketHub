namespace MarketHub.Domain.Specifications;

using Abstractions;
using Infrastructure.Domain.Entities;
using Infrastructure.Specifications;

public sealed class EntityByNameNotDeletedSpecification<TEntity> : Specification<TEntity>
    where TEntity : Entity, ICanBeDeleted, IHasName
{
    public EntityByNameNotDeletedSpecification(string name)
    {
        Query.Where(x => x.Name == name && !x.IsDeleted);
    }
}