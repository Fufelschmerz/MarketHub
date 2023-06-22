namespace MarketHub.Application.Infrastructure.Identity.Policies;

using global::Infrastructure.Application.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;

public static class DefaultPolicy
{
    public static AuthorizationPolicy Create(string authenticationScheme)
        => new AuthorizationPolicyBuilder(authenticationScheme)
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimNames.Sid)
            .Build();
}