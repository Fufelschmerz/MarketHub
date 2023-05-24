namespace Infrastructure.API.Controllers.Abstractions;

using Microsoft.Extensions.Logging;

public interface IHasLogger
{
    ILogger Logger { get; }
}