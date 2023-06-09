namespace MarketHub.API.Controllers.Areas.Common;

using Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;
using Application.Contracts.Common.Account.Requests.PasswordRecovery.CompletePasswordRecovery;
using Application.Contracts.Common.Account.Requests.Registration.BeginRegistration;
using Application.Contracts.Common.Account.Requests.Registration.CompleteRegistration;
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
    [Route("complete-registration")]
    [Authorize]
    public Task<IActionResult> CompleteRegistration(CompleteRegistrationRequest request) =>
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
}