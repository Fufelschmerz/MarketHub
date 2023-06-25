namespace MarketHub.Domain.Specifications.Users.Recoveries;

using Infrastructure.Specifications;
using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Entities.Users.Recoveries;

public sealed class PasswordRecoveryByUserSpecification : Specification<PasswordRecovery>
{
    public PasswordRecoveryByUserSpecification(User user)
    {
        Query.Where(x => x.UserId == user.Id);
    }
}