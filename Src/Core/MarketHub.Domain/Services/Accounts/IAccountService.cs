namespace MarketHub.Domain.Services.Accounts;

using Entities.Accounts;
using Infrastructure.Domain.Services;

public interface IAccountService : IDomainService
{
    Task BeginRegistrationAsync(Account account,
        CancellationToken cancellationToken = default);

    Task CompleteRegistrationAsync(Account account,
        string emailConfirmationToken,
        CancellationToken cancellationToken = default);
}