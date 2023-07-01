namespace MarketHub.Application.Services.Queries.Roles.Handlers;

using Domain.Entities.Users.Roles;
using Domain.Specifications.Users.Roles;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed record FindRolesByTypesQueryHandler : IQueryHandler<FindRolesByTypesQuery, List<Role>>
{
    private readonly IDbRepository<Role> _roleDbRepository;

    public FindRolesByTypesQueryHandler(IDbRepository<Role> roleDbRepository)
    {
        _roleDbRepository = roleDbRepository ?? throw new ArgumentNullException(nameof(roleDbRepository));
    }
    
    public Task<List<Role>> HandleAsync(FindRolesByTypesQuery query,
        CancellationToken cancellationToken = default)
    {
        RolesByTypesSpecification rolesByTypesSpecification = new(query.RoleTypes);

        return _roleDbRepository.GetListAsync(rolesByTypesSpecification,
            cancellationToken);
    }
}