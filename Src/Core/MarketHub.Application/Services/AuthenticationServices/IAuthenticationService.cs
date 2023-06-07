namespace MarketHub.Application.Services.AuthenticationServices;

using global::Infrastructure.Application.Authentication.Data;
using Domain.Entities.Users;

public interface IAuthenticationService
{
    Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    Task<JwtToken?> LoginAsync(string userEmail,
        string userPassword,
        CancellationToken cancellationToken = default);

    Task<JwtToken> RefreshTokenAsync(CancellationToken cancellationToken = default);
}