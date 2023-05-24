namespace Infrastructure.API.Controllers.Extensions;

using Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class ApiControllerExtensions
{
    public static Task<IActionResult> RequestAsync<TRequest, TResponse>(this ApiController apiController,
        TRequest request)
        where TRequest : IRequest<TResponse>
        => apiController.RequestAsync<ApiController, TRequest, TResponse>(request);

    public static Task<IActionResult> RequestAsync<TRequest, TResponse>(this ApiController apiController,
        TRequest request,
        Func<TResponse, IActionResult> success)
        where TRequest : IRequest<TResponse>
        => apiController.RequestAsync<ApiController, TRequest, TResponse>(request,
            success);


    public static Task<IActionResult> RequestAsync<TApiController, TRequest>(this TApiController apiController,
        TRequest request)
        where TApiController :
        ControllerBase,
        IHasDefaultSuccessActionResult,
        IHasDefaultFailActionResult,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasMediator
        where TRequest : IRequest
        => RequestAsync(
            apiController,
            request,
            apiController.Success,
            apiController.Fail);

    public static Task<IActionResult> RequestAsync<TApiController, TRequest>(this TApiController apiController,
        TRequest request,
        Func<IActionResult> success)
        where TApiController :
        ControllerBase,
        IHasUnitOfWork,
        IHasMediator,
        IHasDefaultFailActionResult,
        IHasInvalidModelStateActionResult
        where TRequest : IRequest
        => RequestAsync(
            apiController,
            request,
            success,
            apiController.Fail);

    private static async Task<IActionResult> RequestAsync<TApiController, TRequest>(this TApiController apiController,
        TRequest request,
        Func<IActionResult> success,
        Func<Exception, IActionResult> fail)
        where TApiController :
        ControllerBase,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasMediator
        where TRequest : IRequest
    {
        if (apiController is null)
        {
            throw new ArgumentNullException(nameof(apiController));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (!apiController.ModelState.IsValid)
        {
            return apiController.InvalidModelState(apiController.ModelState);
        }

        try
        {
            await apiController.UnitOfWork.BeginTransaction();

            await apiController.Mediator.Send(request);

            apiController.UnitOfWork.Commit();

            return success();
        }
        catch (Exception exception)
        {
            apiController.UnitOfWork.RollBack();

            return fail(exception);
        }
        finally
        {
            apiController.UnitOfWork.Dispose();
        }
    }

    private static Task<IActionResult> RequestAsync<TApiController, TRequest, TResponse>(
        this TApiController apiController,
        TRequest request)
        where TApiController :
        ControllerBase,
        IHasDefaultResponseSuccessActionResult,
        IHasDefaultFailActionResult,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasMediator
        where TRequest : IRequest<TResponse>
        => RequestAsync(
            apiController,
            request,
            apiController.ResponseSuccess<TResponse>(),
            apiController.Fail);

    private static Task<IActionResult> RequestAsync<TApiController, TRequest, TResponse>(
        this TApiController apiController,
        TRequest request,
        Func<TResponse, IActionResult> success)
        where TApiController :
        ControllerBase,
        IHasDefaultResponseSuccessActionResult,
        IHasDefaultFailActionResult,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasMediator
        where TRequest : IRequest<TResponse>
        => RequestAsync(
            apiController,
            request,
            success,
            apiController.Fail);

    private static async Task<IActionResult> RequestAsync<TApiController, TRequest, TResponse>(
        this TApiController apiController,
        TRequest request,
        Func<TResponse, IActionResult> success,
        Func<Exception, IActionResult> fail)
        where TApiController :
        ControllerBase,
        IHasInvalidModelStateActionResult,
        IHasUnitOfWork,
        IHasMediator
        where TRequest : IRequest<TResponse>
    {
        if (apiController is null)
        {
            throw new ArgumentNullException(nameof(apiController));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (!apiController.ModelState.IsValid)
        {
            return apiController.InvalidModelState(apiController.ModelState);
        }

        try
        {
            await apiController.UnitOfWork.BeginTransaction();

            TResponse response = await apiController.Mediator.Send(request);

            apiController.UnitOfWork.Commit();

            return success(response);
        }
        catch (Exception exception)
        {
            apiController.UnitOfWork.RollBack();

            return fail(exception);
        }
        finally
        {
            apiController.UnitOfWork.Dispose();
        }
    }
}