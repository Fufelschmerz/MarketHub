namespace MarketHub.Application.Services.QueryServices.Users.Roles;

using global::Infrastructure.Application.Services;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Entities.Users.Roles.Enums;

public interface IRoleQueryService : IQueryService<Role>
{
    Task<Role?> FindByRoleTypeAsync(RoleType roleType,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Role>> FindRolesByTypesAsync(IEnumerable<RoleType> roleTypes,
        CancellationToken cancellationToken = default);
}