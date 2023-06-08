namespace MarketHub.Application.Services.AuthenticationServices;

using System.Security.Claims;
using global::Infrastructure.Application.Authentication.Data;

public interface IAuthenticationService
{
    Task<AccessToken> LoginAsync(Claim[] claims);

    Task LogoutAsync();

    Task<AccessToken> RefreshTokenAsync();
}