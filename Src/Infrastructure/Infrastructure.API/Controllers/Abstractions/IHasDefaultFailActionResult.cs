namespace Infrastructure.API.Controllers.Abstractions;

using Microsoft.AspNetCore.Mvc;

public interface IHasDefaultFailActionResult
{
    Func<Exception, IActionResult> Fail { get; }
}