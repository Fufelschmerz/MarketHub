namespace MarketHub.API.Controllers;

using Application.Infrastructure.Exceptions.Constants;
using Application.Infrastructure.Exceptions.Factories;
using Infrastructure.API.Controllers;
using Infrastructure.Application.Exceptions;
using Infrastructure.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public abstract class MarketHubApiController : ApiController
{
    protected MarketHubApiController(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<ApiController> logger)
        : base(unitOfWork,
            mediator,
            logger)
    {
    }

    public override Func<Exception, IActionResult> Fail => FailHandler;

    private IActionResult FailHandler(Exception exception)
    {
        Exception mappedException = ApiExceptionFactory.TryMap(exception);

        switch (mappedException)
        {
            case ApiException apiException:
            {
                int statusCode = GetStatusCode(apiException);

                ProblemDetails problemDetails = new()
                {
                    Title = "Api error",
                    Detail = apiException.Message,
                    Status = statusCode
                };

                problemDetails.Extensions.Add("Id",
                    Guid.NewGuid().ToString());
                
                problemDetails.Extensions.Add("ExtensionCode",
                    apiException.ExtensionCode);

                return StatusCode(statusCode,
                    problemDetails);
            }
            default:
            {
                Logger.LogError(exception, "{ex}", exception);

                int statusCode = StatusCodes.Status500InternalServerError;

                ProblemDetails problemDetails = new()
                {
                    Title = "Unknown error",
                    Detail = mappedException.Message,
                    Status = statusCode
                };
                
                problemDetails.Extensions.Add("Id",
                    Guid.NewGuid().ToString());
                
                problemDetails.Extensions.Add("ExtensionCode",
                    ApiExceptionExtensionCodes.InternalServerError);

                return StatusCode(statusCode,
                    problemDetails);
            }
        }
    }

    private static int GetStatusCode(ApiException exception) =>
        exception.ExtensionCode switch
        {
            ApiExceptionExtensionCodes.InvalidToken => StatusCodes.Status401Unauthorized,
            ApiExceptionExtensionCodes.ObjectNotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };
}