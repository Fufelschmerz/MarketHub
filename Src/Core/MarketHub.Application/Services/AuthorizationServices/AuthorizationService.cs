namespace MarketHub.Application.Services.AuthorizationServices;

using Domain.Entities.Users;
using global::Infrastructure.Application.Authentication.Constants;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Microsoft.AspNetCore.Http;
using Queries.Common;

public sealed class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor,
        IQueryDispatcher queryDispatcher)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
    }

    public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        string sidClaim = _httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type == ClaimNames.Sid).Value!;

        long userId = long.Parse(sidClaim);

        User user = await _queryDispatcher.FindByIdAsync<User?>(userId,
            cancellationToken) ?? throw new ArgumentNullException(nameof(user));

        return user;
    }
}