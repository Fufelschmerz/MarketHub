﻿namespace MarketHub.Application.Services.QueryServices.Roles;

using Domain.Entities.Users.Roles;
using Domain.Entities.Users.Roles.Enums;
using Domain.Repositories.Users.Roles;
using Domain.Specifications.Users.Roles;

public sealed class RoleQueryService : QueryService<Role, IRoleRepository>,
    IRoleQueryService
{
    public RoleQueryService(IRoleRepository repository)
        : base(repository)
    {
    }

    public Task<Role?> FindByRoleTypeAsync(RoleType roleType,
        CancellationToken cancellationToken = default)
    {
        RoleByTypeSpecification roleByTypeSpecification = new(roleType);

        return _repository.SingleOrDefaultAsync(roleByTypeSpecification,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> FindRolesByTypesAsync(IEnumerable<RoleType> roleTypes,
        CancellationToken cancellationToken = default)
    {
        RolesByTypesSpecification rolesByTypesSpecification = new(roleTypes);

        return await _repository.GetListAsync(rolesByTypesSpecification,
            cancellationToken);
    }
}