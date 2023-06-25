namespace MarketHub.Application.Services.Messaging.EmailMessaging.MessageFactory;

using Domain.Entities.Messaging.EmailMessaging;
using Domain.Entities.Messaging.EmailMessaging.Enums;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Queries.Messaging;
using Queries.Messaging.EmailMessaging;

public sealed class EmailMessageFactory : IEmailMessageFactory
{
    private readonly IQueryDispatcher _queryDispatcher;

    public EmailMessageFactory(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
    }
    
    public async Task<(string subject, string body)> CreateEmailConfirmationRequiredMessageAsync(string token,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplate emailMessageTemplate = await _queryDispatcher.FindEmailMessageTemplateByTypeActive(
            EmailMessageTemplateType.EmailConfirmation,
            cancellationToken) ?? throw new ArgumentNullException(nameof(emailMessageTemplate));

        string subject = emailMessageTemplate.Subject;
        string body = string.Format(emailMessageTemplate.Body, token);

        return (subject, body);
    }

    public async Task<(string subject, string body)> CreatePasswordRecoveryMessageAsync(string token,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplate emailMessageTemplate = await _queryDispatcher.FindEmailMessageTemplateByTypeActive(
            EmailMessageTemplateType.PasswordRecovery,
            cancellationToken) ?? throw new ArgumentNullException(nameof(emailMessageTemplate));

        string subject = emailMessageTemplate.Subject;
        string body = string.Format(emailMessageTemplate.Body, token);

        return (subject, body);
    }
}