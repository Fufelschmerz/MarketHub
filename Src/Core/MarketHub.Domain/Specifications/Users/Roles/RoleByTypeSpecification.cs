namespace MarketHub.Domain.Specifications.Users.Roles;

using Entities.Users.Roles;
using Entities.Users.Roles.Enums;
using Infrastructure.Specifications;

public sealed class RoleByTypeSpecification : Specification<Role>
{
    public RoleByTypeSpecification(RoleType type)
    {
        Query.Where(x => x.Type == type);
    }
}