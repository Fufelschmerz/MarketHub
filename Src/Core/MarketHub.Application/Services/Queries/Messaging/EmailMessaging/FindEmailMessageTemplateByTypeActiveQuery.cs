namespace MarketHub.Application.Services.Queries.Messaging.EmailMessaging;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MarketHub.Domain.Entities.Messaging.EmailMessaging.Enums;

public sealed record FindEmailMessageTemplateByTypeActiveQuery(EmailMessageTemplateType EmailMessageTemplateType) : IQuery;

internal static class FindEmailMessageTemplateByTypeActiveQueryExtensions
{
    internal static Task<EmailMessageTemplate?> FindEmailMessageTemplateByTypeActive(
        this IQueryDispatcher queryDispatcher,
        EmailMessageTemplateType emailMessageTemplateType,
        CancellationToken cancellationToken = default)
    {
        return queryDispatcher.ExecuteAsync<FindEmailMessageTemplateByTypeActiveQuery, EmailMessageTemplate?>(
            new FindEmailMessageTemplateByTypeActiveQuery(emailMessageTemplateType),
            cancellationToken);
    }
}