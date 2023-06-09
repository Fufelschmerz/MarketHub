namespace MarketHub.Persistence.Repositories.Accounts.Confirmations;

using Domain.Entities.Accounts.Confirmations;
using Domain.Repositories.Accounts.Confirmations;
using Infrastructure.Specifications.Evaluator.Specification;

public sealed class EmailConfirmationRepository : Repository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(DataContext dataContext,
        ISpecificationEvaluator specificationEvaluator)
        : base(dataContext,
            specificationEvaluator)
    {
    }
}