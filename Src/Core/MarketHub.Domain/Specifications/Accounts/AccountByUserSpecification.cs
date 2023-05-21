namespace MarketHub.Domain.Specifications.Accounts;

using Entities.Accounts;
using Infrastructure.Specifications;

public sealed class AccountByUserSpecification : Specification<Account>
{
    public AccountByUserSpecification(long userId)
    {
        Query.Where(x => x.User.Id == userId);
    }
}