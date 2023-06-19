namespace MarketHub.Application.Services.Commands;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Domain.Entities;

public sealed record DeleteEntityCommand<TEntity>(TEntity Entity) : ICommand
    where TEntity : class, IEntity;

internal static class DeleteEntityCommandExtensions
{
    internal static Task DeleteAsync<TEntity>(this ICommandDispatcher commandDispatcher,
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return commandDispatcher.ExecuteAsync(new DeleteEntityCommand<TEntity>(entity),
            cancellationToken);
    }
}