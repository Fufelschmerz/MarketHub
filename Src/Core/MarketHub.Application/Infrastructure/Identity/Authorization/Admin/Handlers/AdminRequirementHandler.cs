namespace MarketHub.Application.Infrastructure.Identity.Authorization.Admin.Handlers;

using Domain.Entities.Users.Roles.Enums;
using global::Infrastructure.Application.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;
using Requirements;

public sealed class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AdminRequirement requirement)
    {
        string[] userRoles = context.User.Claims.Where(x => x.Type == ClaimNames.Role)
            .Select(x => x.Value)
            .ToArray();
        
        if (userRoles.Contains(RoleType.Admin.ToString()))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}