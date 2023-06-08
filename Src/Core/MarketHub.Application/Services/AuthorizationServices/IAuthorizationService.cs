namespace MarketHub.Application.Services.AuthorizationServices;

using Domain.Entities.Users;

public interface IAuthorizationService
{
    Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}