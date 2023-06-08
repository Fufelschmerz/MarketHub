namespace Infrastructure.Application.Authentication.Builders;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Constants;
using Data;
using Options;
using Microsoft.IdentityModel.Tokens;

public sealed class AccessBuilder : IAccessBuilder
{
    private readonly JwtOptions _jwtOptions;

    public AccessBuilder(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public AccessToken BuildAccessToken(Claim[] claims)
    {
        bool shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims
            .FirstOrDefault(x => x.Type == ClaimNames.Aud)?.Value);

        string? jti = claims.FirstOrDefault(x => x.Type == ClaimNames.Jti)?.Value;

        if (string.IsNullOrWhiteSpace(jti))
            throw new ArgumentNullException(nameof(jti));

        JwtSecurityToken jwtSecurityToken = new(
            _jwtOptions.Issuer,
            shouldAddAudienceClaim ? _jwtOptions.Audience : string.Empty,
            claims,
            expires: DateTime.UtcNow.Add(_jwtOptions.AccessTokenExpires),
            signingCredentials: new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature));

        string jwt = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        RefreshToken refreshToken = new(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            DateTime.UtcNow,
            _jwtOptions.RefreshTokenExpires);

        return new AccessToken(jti,
            jwt,
            _jwtOptions.AccessTokenExpires,
            refreshToken);
    }
}