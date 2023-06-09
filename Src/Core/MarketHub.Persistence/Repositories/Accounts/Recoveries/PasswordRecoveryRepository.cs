namespace MarketHub.Persistence.Repositories.Accounts.Recoveries;

using Domain.Entities.Accounts.Recoveries;
using Domain.Repositories.Accounts.Recoveries;
using Infrastructure.Specifications.Evaluator.Specification;

public sealed class PasswordRecoveryRepository : Repository<PasswordRecovery>, IPasswordRecoveryRepository
{
    public PasswordRecoveryRepository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
        : base(dataContext,
            specificationEvaluator)
    {
    }
}