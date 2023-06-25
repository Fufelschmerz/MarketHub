namespace MarketHub.Domain.Specifications.Accounts.Confirmations;

using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Infrastructure.Specifications;

public sealed class EmailConfirmationByAccountSpecification : Specification<EmailConfirmation>
{
    public EmailConfirmationByAccountSpecification(Account account)
    {
        Query.Where(x => x.AccountId == account.Id);
    }
}