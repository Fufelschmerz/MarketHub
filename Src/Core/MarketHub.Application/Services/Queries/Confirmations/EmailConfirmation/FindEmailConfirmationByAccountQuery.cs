namespace MarketHub.Application.Services.Queries.Confirmations.EmailConfirmation;

using Domain.Entities.Accounts;
using Domain.Entities.Accounts.Confirmations;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;

public sealed record FindEmailConfirmationByAccountQuery(Account Account) : IQuery;

internal static class FindEmailConfirmationByAccountQueryExtensions
{
    internal static Task<EmailConfirmation?> FindEmailConfirmationByAccountAsync(this IQueryDispatcher queryDispatcher,
        Account account,
        CancellationToken cancellationToken = default)
    {
        return queryDispatcher.ExecuteAsync<FindEmailConfirmationByAccountQuery, EmailConfirmation?>(
            new FindEmailConfirmationByAccountQuery(account),
            cancellationToken);
    }
}