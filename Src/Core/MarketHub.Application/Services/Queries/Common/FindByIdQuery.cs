namespace MarketHub.Application.Services.Queries.Common;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;

public sealed record FindByIdQuery(long Id) : IQuery;

internal static class FindByIdQueryExtensions
{
    internal static Task<TResult> FindByIdAsync<TResult>(this IQueryDispatcher queryDispatcher,
        long id,
        CancellationToken cancellationToken = default)
    {
        return queryDispatcher.ExecuteAsync<FindByIdQuery, TResult>(new FindByIdQuery(id),
            cancellationToken);
    }
}