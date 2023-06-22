namespace MarketHub.API.Controllers.Areas.Common;

using Application.Contracts.Common.Authentication.Requests.Login;
using Application.Contracts.Common.Authentication.Requests.Logout;
using Application.Contracts.Common.Authentication.Requests.RefreshToken;
using Constants;
using Infrastructure.API.Controllers;
using Infrastructure.API.Controllers.Extensions;
using Infrastructure.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area(Areas.Common)]
[Route("api/common/authentication")]
public sealed class AuthenticationController : MarketHubApiController
{
    public AuthenticationController(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ApiController> logger)
        : base(unitOfWork,
            mediator,
            logger)
    {
    }
    
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Login(LoginRequest request) =>
        this.RequestAsync<LoginRequest, LoginResponse>(request);
    
    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public Task<IActionResult> RefreshToken(RefreshTokenRequest request) =>
        this.RequestAsync<RefreshTokenRequest, RefreshTokenResponse>(request);
    
    [HttpPost]
    [Route("logout")]
    [Authorize]
    public Task<IActionResult> Logout(LogoutRequest request) =>
        this.RequestAsync(request);
}