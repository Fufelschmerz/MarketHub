namespace Infrastructure.Application.Services.Commands.Dispatchers;

public interface ICommandDispatcher
{
    Task ExecuteAsync<TCommand>(TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}