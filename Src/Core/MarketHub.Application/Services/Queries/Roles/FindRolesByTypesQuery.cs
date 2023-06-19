namespace MarketHub.Application.Services.Queries.Roles;

using Domain.Entities.Users.Roles;
using Domain.Entities.Users.Roles.Enums;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;

public sealed record FindRolesByTypesQuery(IEnumerable<RoleType> RoleTypes) : IQuery;

internal static class FindRolesByTypesQueryExtensions
{
    internal static async Task<IReadOnlyList<Role>> FindRolesByTypesAsync(this IQueryDispatcher queryDispatcher,
        IEnumerable<RoleType> roleTypes,
        CancellationToken cancellationToken = default)
    {
        return await queryDispatcher.ExecuteAsync<FindRolesByTypesQuery, List<Role>>(
            new FindRolesByTypesQuery(roleTypes),
            cancellationToken);
    }
}