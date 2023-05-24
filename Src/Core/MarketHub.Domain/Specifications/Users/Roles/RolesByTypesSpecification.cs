namespace MarketHub.Domain.Specifications.Users.Roles;

using Entities.Users.Roles;
using Entities.Users.Roles.Enums;
using Infrastructure.Specifications;

public sealed class RolesByTypesSpecification : Specification<Role>
{
    public RolesByTypesSpecification(IEnumerable<RoleType> roleTypes)
    {
        Query.Where(x => roleTypes.Contains(x.Type));
    }
}