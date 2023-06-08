namespace MarketHub.API.Controllers.Areas.Common;

using Application.Contracts.Common.Account.Requests.BeginRegistration;
using Application.Contracts.Common.Account.Requests.Login;
using Application.Contracts.Common.Account.Requests.Logout;
using Application.Contracts.Common.Account.Requests.RefreshToken;
using Constants;
using Infrastructure.API.Controllers;
using Infrastructure.API.Controllers.Extensions;
using Infrastructure.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area(Areas.Common)]
[Route("api/common/account")]
public sealed class AccountController : MarketHubApiController
{
    public AccountController(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ApiController> logger)
        : base(unitOfWork,
            mediator,
            logger)
    {
    }

    [HttpPost]
    [Route("begin-registration")]
    [AllowAnonymous]
    public Task<IActionResult> BeginRegistration(BeginRegistrationRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Login(LoginRequest request) =>
        this.RequestAsync<LoginRequest, LoginResponse>(request);

    [HttpPost]
    [Route("refresh-token")]
    [Authorize]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RefreshToken(RefreshTokenRequest request) =>
        this.RequestAsync<RefreshTokenRequest, RefreshTokenResponse>(request);

    [HttpPost]
    [Route("logout")]
    [Authorize]
    public Task<IActionResult> Logout(LogoutRequest request) =>
        this.RequestAsync(request);
}