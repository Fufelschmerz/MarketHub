namespace Infrastructure.Application.Services.Queries.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery;
}