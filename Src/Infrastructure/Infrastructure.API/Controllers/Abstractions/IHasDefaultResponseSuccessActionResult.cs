namespace Infrastructure.API.Controllers.Abstractions;

using Microsoft.AspNetCore.Mvc;

public interface IHasDefaultResponseSuccessActionResult
{
    Func<TResponse, IActionResult> ResponseSuccess<TResponse>();
}