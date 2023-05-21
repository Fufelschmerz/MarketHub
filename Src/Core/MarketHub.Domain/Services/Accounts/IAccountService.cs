namespace MarketHub.Domain.Services.Accounts;

using Entities.Accounts;
using Entities.Users;
using Infrastructure.Domain.Services;

public interface IAccountService : IDomainService
{
    Task<Account> BeginRegistrationAsync(User user,
        CancellationToken cancellationToken = default);

    Task CompleteRegistrationAsync(Account account, string emailConfirmationToken,
        CancellationToken cancellationToken = default);
}