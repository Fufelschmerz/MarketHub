namespace MarketHub.Domain.Specifications.Users.Roles;

using Entities.Users.Roles;
using Entities.Users.Roles.Enums;
using Infrastructure.Specifications;

public sealed class RoleByTypeSpecification : Specification<Role>
{
    public RoleByTypeSpecification(RoleType roleType)
    {
        Query.Where(x => x.RoleType == roleType);
    }
}