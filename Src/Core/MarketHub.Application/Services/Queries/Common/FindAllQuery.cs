namespace MarketHub.Application.Services.Queries.Common;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using global::Infrastructure.Domain.Entities;

public sealed record FindAllQuery : IQuery;

internal static class FindAllQueryExtensions
{
    internal static async Task<IReadOnlyList<TEntity>> FindAllAsync<TEntity>(this IQueryDispatcher queryDispatcher,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return await queryDispatcher.ExecuteAsync<FindAllQuery, List<TEntity>>(new FindAllQuery(),
            cancellationToken);
    }
}