namespace MarketHub.Domain.Specifications.Accounts.Confirmations;

using Entities.Accounts.Confirmations;
using Infrastructure.Specifications;

public sealed class EmailConfirmationByTokenSpecification : Specification<EmailConfirmation>
{
    public EmailConfirmationByTokenSpecification(string token)
    {
        Query.Where(x => x.Token == token);
    }
}