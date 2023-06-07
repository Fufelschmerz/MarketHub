namespace MarketHub.Application.Services.AuthenticationServices;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using global::Infrastructure.Application.Authentication.Constants;
using global::Infrastructure.Application.Authentication.Data;
using QueryServices.Users;
using Domain.Entities.Users;
using global::Infrastructure.Application.Authentication.Builders;
using Infrastructure.Identity.Claims.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public sealed class AuthenticationService : IAuthenticationService
{
    private const string RefreshTokenCookieKey = "X-RefreshToken";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtTokenBuilder _jwtTokenBuilder;
    private readonly IUserQueryService _userQueryService;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor,
        IJwtTokenBuilder jwtTokenBuilder,
        IUserQueryService userQueryService,
        IOptions<TokenValidationParameters> tokenValidationParameters)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _jwtTokenBuilder = jwtTokenBuilder ?? throw new ArgumentNullException(nameof(jwtTokenBuilder));
        _userQueryService = userQueryService ?? throw new ArgumentNullException(nameof(userQueryService));
        _tokenValidationParameters = tokenValidationParameters.Value ?? throw new ArgumentNullException(nameof(tokenValidationParameters.Value));
    }

    public Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        Claim? sidClaim = _httpContextAccessor.HttpContext?.User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimNames.Sid);

        if (sidClaim == null)
            return Task.FromResult<User?>(null);

        if (!long.TryParse(sidClaim.Value,
                out long id))
            return Task.FromResult<User?>(null);

        return _userQueryService.FindByIdAsync(id,
            cancellationToken);
    }

    public async Task<JwtToken?> LoginAsync(string userEmail,
        string userPassword,
        CancellationToken cancellationToken = default)
    {
        User? user = await _userQueryService.FindByEmailAsync(userEmail,
            cancellationToken);

        if (user is null || !user.Password.Check(userPassword))
            return null;

        Claim[] claims = ClaimFactory.CreateClaims(user).ToArray();

        JwtToken jwtToken = _jwtTokenBuilder.BuildJwtToken(claims);
        
        SetRefreshTokenToCookies(jwtToken.RefreshToken);
        
        return jwtToken;
    }

    public Task<JwtToken> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        //TODO: сдлеать реализацию
        throw new NotImplementedException();
    }
    
    private (ClaimsPrincipal claimsPrincipal, JwtSecurityToken jwtSecurityToken) DecodeJwtToken(string jwtToken)
    {
        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(jwtToken, _tokenValidationParameters,
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