namespace Infrastructure.Application.Services.Commands.Dispatchers;

using Microsoft.Extensions.DependencyInjection;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _resolver;

    public CommandDispatcher(IServiceProvider resolver)
    {
        _resolver = resolver ??
                    throw new ArgumentNullException(nameof(resolver));
    }

    public Task ExecuteAsync<TCommand>(TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var handler = _resolver.GetService<ICommandHandler<TCommand>>();

        if (handler == null)
            throw new ArgumentNullException(nameof(handler));

        return handler.HandleAsync(command,
            cancellationToken);
    }
}