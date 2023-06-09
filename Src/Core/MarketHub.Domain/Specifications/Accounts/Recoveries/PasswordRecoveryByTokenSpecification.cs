namespace MarketHub.Domain.Specifications.Accounts.Recoveries;

using Entities.Accounts.Recoveries;
using Infrastructure.Specifications;

public sealed class PasswordRecoveryByTokenSpecification : Specification<PasswordRecovery>
{
    public PasswordRecoveryByTokenSpecification(string token)
    {
        Query.Where(x => x.Token == token)
            .Include(x => x.Account);
    }
}