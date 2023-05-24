namespace Infrastructure.API.Controllers.Abstractions;

using Microsoft.AspNetCore.Mvc;

public interface IHasDefaultSuccessActionResult
{
    Func<IActionResult> Success { get; }
}