namespace MarketHub.Application.Services.AuthorizationServices;

using Domain.Entities.Users;
using global::Infrastructure.Application.Authentication.Constants;
using Microsoft.AspNetCore.Http;
using QueryServices.Users;

public sealed class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserQueryService _userQueryService;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor,
        IUserQueryService userQueryService)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userQueryService = userQueryService ?? throw new ArgumentNullException(nameof(userQueryService));
    }

    public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        string sidClaim = _httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type == ClaimNames.Sid).Value!;

        long userId = long.Parse(sidClaim);

        User user = await _userQueryService.FindByIdAsync(userId,
            cancellationToken) ?? throw new ArgumentNullException(nameof(user));

        return user;
    }
}