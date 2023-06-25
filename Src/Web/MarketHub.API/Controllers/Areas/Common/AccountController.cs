namespace MarketHub.API.Controllers.Areas.Common;

using Application.Contracts.Common.Account.Requests.EmailConfirmation.BeginEmailConfirmation;
using Application.Contracts.Common.Account.Requests.EmailConfirmation.CompleteEmailConfirmation;
using Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;
using Application.Contracts.Common.Account.Requests.PasswordRecovery.CompletePasswordRecovery;
using Application.Contracts.Common.Account.Requests.Registration;
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
    [Route("registration")]
    [AllowAnonymous]
    public Task<IActionResult> Registration(RegistrationRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("begin-password-recovery")]
    [AllowAnonymous]
    public Task<IActionResult> BeginPasswordRecovery(BeginPasswordRecoveryRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("complete-password-recovery")]
    [AllowAnonymous]
    public Task<IActionResult> CompletePasswordRecovery(CompletePasswordRecoveryRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("begin-email-confirmation")]
    [Authorize]
    public Task<IActionResult> BeginEmailConfirmation(BeginEmailConfirmationRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("complete-email-confirmation")]
    [Authorize]
    public Task<IActionResult> CompleteEmailConfirmation(CompleteEmailConfirmationRequest request) =>
        this.RequestAsync(request);
}