namespace MarketHub.Application.Services.Queries.Users;

using Domain.Entities.Users;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;

public sealed record FindUserByEmailQuery(string Email) : IQuery;

internal static class FindUserByEmailQueryExtensions
{
    internal static Task<User?> FindUserByEmailAsync(this IQueryDispatcher queryDispatcher,
        string email,
        CancellationToken cancellationToken = default)
    {
        return queryDispatcher.ExecuteAsync<FindUserByEmailQuery, User?>(new FindUserByEmailQuery(email),
            cancellationToken);
    }
}