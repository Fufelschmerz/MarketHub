namespace MarketHub.Persistence.Repositories.Users;

using Infrastructure.Specifications.Evaluator.Specification;
using MarketHub.Domain.Entities.Users;
using Repositories;
using Microsoft.EntityFrameworkCore;

public sealed class UserDbRepository : DbRepository<User>
{
    public UserDbRepository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
        : base(dataContext,
            specificationEvaluator)
    {
    }

    public override async Task<User?> GetByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        return await _entities.Include(x => x.Account)
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }
}