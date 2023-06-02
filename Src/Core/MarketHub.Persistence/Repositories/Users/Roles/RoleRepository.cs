namespace MarketHub.Persistence.Repositories.Users.Roles;

using Infrastructure.Specifications.Evaluator.Specification;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Repositories.Users.Roles;
using Persistence;
using Repositories;

public sealed class RoleRepository : Repository<Role>,
    IRoleRepository
{
    public RoleRepository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
        : base(dataContext,
            specificationEvaluator)
    {
    }
}