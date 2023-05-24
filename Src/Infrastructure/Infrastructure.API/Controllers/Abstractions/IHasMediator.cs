namespace Infrastructure.API.Controllers.Abstractions;

using MediatR;

public interface IHasMediator
{
    IMediator Mediator { get; }
}