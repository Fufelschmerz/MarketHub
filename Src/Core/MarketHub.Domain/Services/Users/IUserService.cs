namespace MarketHub.Domain.Services.Users;

using Infrastructure.Domain.Services;
using MarketHub.Domain.Entities.Users;

public interface IUserService : IDomainService
{
    Task CreateAsync(User user,
        CancellationToken cancellationToken = default);
}