namespace Infrastructure.API.Controllers;

using Abstractions;
using Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

[ApiController]
public abstract class ApiController
    : ControllerBase,
        IHasMediator,
        IHasDefaultSuccessActionResult,
        IHasDefaultResponseSuccessActionResult,
        IHasDefaultFailActionResult,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasLogger
{
    protected ApiController(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ApiController> logger)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IMediator Mediator { get; }

    public IUnitOfWork UnitOfWork { get; }
    
    public ILogger Logger { get; }

    public virtual Func<IActionResult> Success
        => () => new OkResult();

    public virtual Func<Exception, IActionResult> Fail
        => (exception) => new BadRequestObjectResult(exception.Message);

    public virtual Func<ModelStateDictionary, IActionResult> InvalidModelState
        => (modelState) => new BadRequestObjectResult(new ValidationProblemDetails(modelState).Errors);

    public virtual Func<TResponse, IActionResult> ResponseSuccess<TResponse>()
        => (response) => new OkObjectResult(response);
}