namespace Infrastructure.Application.Authentication.Builders;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Constants;
using Data;
using Options;
using Microsoft.IdentityModel.Tokens;

public sealed class JwtTokenBuilder : IJwtTokenBuilder
{
    private readonly JwtTokenOptions _jwtTokenOptions;

    public JwtTokenBuilder(JwtTokenOptions jwtTokenOptions)
    {
        _jwtTokenOptions = jwtTokenOptions ?? throw new ArgumentNullException(nameof(jwtTokenOptions));
    }

    public JwtToken BuildJwtToken(Claim[] claims)
    {
        bool shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims
            .FirstOrDefault(x => x.Type == ClaimNames.Aud)?.Value);

        string? jti = claims.FirstOrDefault(x => x.Type == ClaimNames.Jti)?.Value;

        if (string.IsNullOrWhiteSpace(jti))
            throw new ArgumentNullException(nameof(jti));

        JwtSecurityToken jwtSecurityToken = new(
            _jwtTokenOptions.Issuer,
            shouldAddAudienceClaim ? _jwtTokenOptions.Audience : string.Empty,
            claims,
            expires: DateTime.UtcNow.Add(_jwtTokenOptions.AccessTokenExpires),
            signingCredentials: new SigningCredentials(_jwtTokenOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature));

        string jwtToken = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        RefreshToken refreshToken = new(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            DateTime.UtcNow,
            _jwtTokenOptions.RefreshTokenExpires);

        return new JwtToken(jti,
            jwtToken,
            refreshToken);
    }
}