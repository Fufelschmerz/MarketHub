namespace MarketHub.Domain.Specifications;

using Abstractions;
using Infrastructure.Domain.Entities;
using Infrastructure.Specifications;

public sealed class EntityByNameSpecification<TEntity> : Specification<TEntity>
    where TEntity : Entity, IHasName
{
    public EntityByNameSpecification(string name)
    {
        Query.Where(x => x.Name == name);
    }
}