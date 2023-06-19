namespace MarketHub.Application.Services.Commands;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Domain.Entities;

public sealed record CreateEntityCommand<TEntity>(TEntity Entity) : ICommand
    where TEntity : class, IEntity;

internal static class CreateObjectWithIdCommandContextExtensions
{
    internal static Task CreateAsync<TEntity>(this ICommandDispatcher commandDispatcher,
        TEntity objectWithId,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return commandDispatcher.ExecuteAsync(new CreateEntityCommand<TEntity>(objectWithId),
            cancellationToken);
    }
}