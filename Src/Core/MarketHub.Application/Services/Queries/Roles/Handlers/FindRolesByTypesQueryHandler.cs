namespace MarketHub.Application.Services.Queries.Roles.Handlers;

using Domain.Entities.Users.Roles;
using Domain.Specifications.Users.Roles;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed record FindRolesByTypesQueryHandler : IQueryHandler<FindRolesByTypesQuery, List<Role>>
{
    private readonly IRepository<Role> _roleRepository;

    public FindRolesByTypesQueryHandler(IRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }
    
    public Task<List<Role>> HandleAsync(FindRolesByTypesQuery query,
        CancellationToken cancellationToken = default)
    {
        RolesByTypesSpecification rolesByTypesSpecification = new(query.RoleTypes);

        return _roleRepository.GetListAsync(rolesByTypesSpecification,
            cancellationToken);
    }
}