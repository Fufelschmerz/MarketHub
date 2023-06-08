namespace MarketHub.Application.Services.AuthorizationServices;

using System.Security.Claims;
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
    
    public Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        Claim? sidClaim = _httpContextAccessor.HttpContext?.User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimNames.Sid);
        
        if (!long.TryParse(sidClaim?.Value, out long id))
            return Task.FromResult<User?>(null);

        return _userQueryService.FindByIdAsync(id,
            cancellationToken);
    }
}