namespace MarketHub.Application.Infrastructure.Identity.Claims.Factories;

using System.Security.Claims;
using Domain.Entities.Users;
using global::Infrastructure.Application.Authentication.Constants;

internal static class ClaimFactory
{
    internal static IEnumerable<Claim> CreateClaims(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimNames.Sid,
                user.Id.ToString()),
            new Claim(ClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

        claims.AddRange(user.Roles.Select(x => new Claim(ClaimNames.Role,
            x.RoleType.ToString())));

        return claims;
    }
}