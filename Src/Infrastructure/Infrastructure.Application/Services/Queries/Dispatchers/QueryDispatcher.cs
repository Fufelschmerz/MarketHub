namespace Infrastructure.Application.Services.Queries.Dispatchers;

using Microsoft.Extensions.DependencyInjection;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _resolver;

    public QueryDispatcher(IServiceProvider resolver)
    {
        _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
    }

    public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery
    {
        IQueryHandler<TQuery, TResult>? queryHandler = _resolver.GetService<IQueryHandler<TQuery, TResult>>();

        if (queryHandler is null)
            throw new ArgumentNullException(nameof(queryHandler));

        return queryHandler.HandleAsync(query,
            cancellationToken);
    }
}