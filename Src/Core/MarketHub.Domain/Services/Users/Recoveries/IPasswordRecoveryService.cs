namespace MarketHub.Domain.Services.Users.Recoveries;

using Entities.Users;
using Infrastructure.Domain.Services;

public interface IPasswordRecoveryService : IDomainService
{
    Task CreateAsync(User user,
        CancellationToken cancellationToken = default);

    Task RecoverAsync(string token,
        string newPassword,
        CancellationToken cancellationToken = default);
}