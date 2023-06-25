namespace MarketHub.Domain.Specifications.Users.Roles;

using Infrastructure.Specifications;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Entities.Users.Roles.Enums;

public sealed class RolesByTypesSpecification : Specification<Role>
{
    public RolesByTypesSpecification(IEnumerable<RoleType> roleTypes)
    {
        Query.Where(x => roleTypes.Contains(x.RoleType));
    }
}