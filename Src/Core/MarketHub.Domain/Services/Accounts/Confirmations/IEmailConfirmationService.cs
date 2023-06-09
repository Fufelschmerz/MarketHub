namespace MarketHub.Domain.Services.Accounts.Confirmations;

using Entities.Accounts;
using Infrastructure.Domain.Services;

public interface IEmailConfirmationService : IDomainService
{
    Task CreateAsync(Account account,
        CancellationToken cancellationToken = default);

    Task ConfirmAsync(string token,
        CancellationToken cancellationToken = default);
}