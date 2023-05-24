namespace MarketHub.Application.Services.QueryServices.Roles;

using Domain.Entities.Users.Roles;
using Domain.Entities.Users.Roles.Enums;
using global::Infrastructure.Application.Services;

public interface IRoleQueryService : IQueryService<Role>
{
    Task<Role?> FindByRoleTypeAsync(RoleType roleType,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Role>> FindRolesByTypesAsync(IEnumerable<RoleType> roleTypes,
        CancellationToken cancellationToken = default);
}