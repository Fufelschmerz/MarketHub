namespace MarketHub.Domain.Services.Users;

using Infrastructure.Domain.Services;
using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Services.Common.UniqueName;

public interface IUserService : IDomainService,
    IUniqueNameService<User>
{
    Task<User> CreateAsync(string name,
        string email,
        string password,
        IEnumerable<Role> roles,
        CancellationToken cancellationToken = default);
}