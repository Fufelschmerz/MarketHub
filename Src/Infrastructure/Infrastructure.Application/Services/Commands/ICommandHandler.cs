namespace Infrastructure.Application.Services.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command,
        CancellationToken cancellationToken = default);
}