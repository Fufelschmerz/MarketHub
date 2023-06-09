namespace MarketHub.Persistence.Repositories.Accounts;

using Domain.Entities.Accounts;
using Domain.Repositories.Accounts;
using Infrastructure.Specifications.Evaluator.Specification;

public sealed class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
        : base(dataContext,
            specificationEvaluator)
    {
    }
}