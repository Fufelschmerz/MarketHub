namespace MarketHub.Application.Services.Commands;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Domain.Entities;

public sealed record UpdateEntityCommand<TEntity>(TEntity Entity) : ICommand
    where TEntity : class, IEntity;

internal static class UpdateEntityCommandExtensions
{
    internal static Task UpdateAsync<TEntity>(this ICommandDispatcher commandDispatcher,
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return commandDispatcher.ExecuteAsync(new UpdateEntityCommand<TEntity>(entity),
            cancellationToken);
    }
}