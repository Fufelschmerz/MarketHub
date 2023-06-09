namespace MarketHub.Domain.Services.Accounts;

using Entities.Accounts;
using Infrastructure.Domain.Services;

public interface IAccountService : IDomainService
{
    Task RegistrationAsync(Account account,
        CancellationToken cancellationToken = default);
}