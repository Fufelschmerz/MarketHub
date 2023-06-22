namespace MarketHub.API.Controllers.Areas.Admin.Messaging;

using Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Activate;
using Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Create;
using Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetById;
using Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetList;
using Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Update;
using Application.Infrastructure.Identity.Policies;
using Constants;
using Infrastructure.API.Controllers;
using Infrastructure.API.Controllers.Extensions;
using Infrastructure.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area(Areas.Admin)]
[Route("api/admin/emailMessageTemplate")]
[Authorize(Policy = Policy.Admin)]
public sealed class EmailMessageTemplateController : MarketHubApiController
{
    public EmailMessageTemplateController(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ApiController> logger)
        : base(unitOfWork,
            mediator,
            logger)
    {
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(EmailMessageTemplateCreateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create(EmailMessageTemplateCreateRequest request) =>
        this.RequestAsync<EmailMessageTemplateCreateRequest, EmailMessageTemplateCreateResponse>(request);

    [HttpPost]
    [Route("update")]
    public Task<IActionResult> Update(EmailMessageTemplateUpdateRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("activate")]
    public Task<IActionResult> Activate(EmailMessageTemplateActivateRequest request) =>
        this.RequestAsync(request);

    [HttpPost]
    [Route("getById")]
    [ProducesResponseType(typeof(EmailMessageTemplateGetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetById(EmailMessageTemplateGetByIdRequest request) =>
        this.RequestAsync<EmailMessageTemplateGetByIdRequest, EmailMessageTemplateGetByIdResponse>(request);

    [HttpPost]
    [Route("getList")]
    [ProducesResponseType(typeof(EmailMessageTemplateGetListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetList(EmailMessageTemplateGetListRequest request) =>
        this.RequestAsync<EmailMessageTemplateGetListRequest, EmailMessageTemplateGetListResponse>(request);
}