namespace MarketHub.Domain.Services.Users;

using Infrastructure.Domain.Services;
using MarketHub.Domain.Entities.Users;
using Common.UniqueName;

public interface IUserService : IDomainService,
    IUniqueNameService<User>
{
    Task CreateAsync(User user,
        CancellationToken cancellationToken = default);
}