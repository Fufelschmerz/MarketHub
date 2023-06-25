namespace MarketHub.Application.Services.Queries.PasswordRecoveries;

using Domain.Entities.Users;
using Domain.Entities.Users.Recoveries;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;

public sealed record FindPasswordRecoveryByUserQuery(User User) : IQuery;

internal static class FindPasswordRecoveryByUserQueryExtensions
{
    internal static Task<PasswordRecovery?> FindPasswordRecoveryByUserAsync(this IQueryDispatcher queryDispatcher,
        User user,
        CancellationToken cancellationToken = default)
    {
        return queryDispatcher.ExecuteAsync<FindPasswordRecoveryByUserQuery, PasswordRecovery?>(
            new FindPasswordRecoveryByUserQuery(user),
            cancellationToken);
    }
}