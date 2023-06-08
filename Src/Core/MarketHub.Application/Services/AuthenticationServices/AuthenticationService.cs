namespace MarketHub.Application.Services.AuthenticationServices;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using global::Infrastructure.Application.Authentication.Data;
using global::Infrastructure.Application.Authentication.Builders;
using global::Infrastructure.Application.Authentication.Constants;
using global::Infrastructure.Cache.Services;
using Infrastructure.Cache;
using Infrastructure.Exceptions.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

public sealed class AuthenticationService : IAuthenticationService
{
    private const string RefreshTokenCookieKey = "X-RefreshToken";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccessBuilder _accessBuilder;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly ICacheService<RefreshToken> _refreshTokenCacheService;
    private readonly ICacheService<AccessToken> _accessTokenCacheService;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor,
        IAccessBuilder accessBuilder,
        IOptions<TokenValidationParameters> tokenValidationParameters,
        ICacheService<RefreshToken> refreshTokenCacheService,
        ICacheService<AccessToken> accessTokenCacheService)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _accessBuilder = accessBuilder ?? throw new ArgumentNullException(nameof(accessBuilder));
        _tokenValidationParameters = tokenValidationParameters.Value ?? throw new ArgumentNullException(nameof(tokenValidationParameters.Value));
        _refreshTokenCacheService = refreshTokenCacheService ?? throw new ArgumentNullException(nameof(refreshTokenCacheService));
        _accessTokenCacheService = accessTokenCacheService ?? throw new ArgumentNullException(nameof(accessTokenCacheService));
    }

    public async Task<AccessToken> LoginAsync(Claim[] claims)
    {
        AccessToken accessToken = _accessBuilder.BuildAccessToken(claims);

        SetRefreshTokenToCookies(accessToken.RefreshToken);

        await _refreshTokenCacheService.AddAsync(Keys.GetRefreshTokenKey(accessToken.Jti),
            accessToken.RefreshToken,
            accessToken.RefreshToken.Expires);

        await _accessTokenCacheService.AddAsync(Keys.GetAccessTokenKey(accessToken.Jti),
            accessToken,
            accessToken.Expires);

        return accessToken;
    }

    public async Task LogoutAsync()
    {
        string? jti = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimNames.Jti)?
            .Value;

        if (string.IsNullOrWhiteSpace(jti))
            throw new ArgumentNullException(nameof(jti));

        await _accessTokenCacheService.DeleteAsync(Keys.GetAccessTokenKey(jti));
        await _refreshTokenCacheService.DeleteAsync(Keys.GetRefreshTokenKey(jti));
    }

    public async Task<AccessToken> RefreshTokenAsync()
    {
        string? authorization = _httpContextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization];

        if (!AuthenticationHeaderValue.TryParse(authorization, out AuthenticationHeaderValue? authenticationHeaderValue))
            throw new ArgumentException("Invalid authorization header", nameof(authorization));

        if (authenticationHeaderValue is null)
            throw new ArgumentNullException(nameof(authenticationHeaderValue));

        if (string.IsNullOrWhiteSpace(authenticationHeaderValue.Parameter))
            throw ApiExceptionFactory.InvalidToken(nameof(AccessToken.Jwt));

        string? refreshTokenValue = _httpContextAccessor.HttpContext?.Request.Cookies[RefreshTokenCookieKey];

        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            throw ApiExceptionFactory.InvalidToken(nameof(RefreshToken));

        (ClaimsPrincipal claimsPrincipal, JwtSecurityToken jwtSecurityToken) decodedJwt = DecodeJwt(authenticationHeaderValue.Parameter);

        if (!decodedJwt.jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            throw ApiExceptionFactory.InvalidToken(nameof(AccessToken.Jwt));

        Claim[] claims = decodedJwt.claimsPrincipal.Claims.ToArray();

        string? jti = claims.FirstOrDefault(x => x.Type == ClaimNames.Jti)?.Value;

        if (string.IsNullOrWhiteSpace(jti))
            throw new ArgumentNullException(nameof(jti));

        RefreshToken? refreshToken = await _refreshTokenCacheService
            .GetAsync(Keys.GetRefreshTokenKey(jti));

        if (refreshToken is null || !refreshToken.IsExpired())
            throw ApiExceptionFactory.InvalidToken(nameof(RefreshToken));

        return await LoginAsync(claims);
    }

    private (ClaimsPrincipal claimsPrincipal, JwtSecurityToken jwtSecurityToken) DecodeJwt(string jwt)
    {
        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwt,
            _tokenValidationParameters,
            out SecurityToken validatedToken);

        return (claimsPrincipal, validatedToken as JwtSecurityToken)!;
    }

    private void SetRefreshTokenToCookies(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = DateTime.UtcNow.Add(refreshToken.Expires),
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(RefreshTokenCookieKey,
            refreshToken.Token,
            cookieOptions);
    }
}