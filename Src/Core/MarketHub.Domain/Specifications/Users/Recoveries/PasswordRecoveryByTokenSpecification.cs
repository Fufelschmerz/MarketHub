namespace MarketHub.Domain.Specifications.Users.Recoveries;

using Infrastructure.Specifications;
using MarketHub.Domain.Entities.Users.Recoveries;

public sealed class PasswordRecoveryByTokenSpecification : Specification<PasswordRecovery>
{
    public PasswordRecoveryByTokenSpecification(string token)
    {
        Query.Where(x => x.Token == token)
            .Include(x => x.User);
    }
}