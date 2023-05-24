namespace MarketHub.Application.Services.QueryServices.Users;

using Domain.Entities.Users;
using global::Infrastructure.Application.Services;

public interface IUserQueryService : IQueryService<User>
{
    Task<User?> FindByEmailAsync(string email,
        CancellationToken cancellationToken = default);
}