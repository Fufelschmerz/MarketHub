namespace MarketHub.Domain.Specifications.Users.Roles;

using Infrastructure.Specifications;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Entities.Users.Roles.Enums;

public sealed class RoleByTypeSpecification : Specification<Role>
{
    public RoleByTypeSpecification(RoleType roleType)
    {
        Query.Where(x => x.RoleType == roleType);
    }
}