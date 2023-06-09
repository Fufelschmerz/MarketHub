namespace MarketHub.Domain.Services.Accounts.Recoveries;

using Entities.Accounts;
using Infrastructure.Domain.Services;

public interface IPasswordRecoveryService : IDomainService
{
    Task CreateAsync(Account account,
        CancellationToken cancellationToken = default);

    Task RecoverAsync(string token,
        string newPassword,
        CancellationToken cancellationToken = default);
}