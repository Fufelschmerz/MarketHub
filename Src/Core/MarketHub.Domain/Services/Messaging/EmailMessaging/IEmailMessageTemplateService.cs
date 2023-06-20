namespace MarketHub.Domain.Services.Messaging.EmailMessaging;

using Entities.Messaging.EmailMessaging;
using Infrastructure.Domain.Services;

public interface IEmailMessageTemplateService : IDomainService
{
    Task ActivateAsync(EmailMessageTemplate emailMessageTemplate, 
        CancellationToken cancellationToken = default);
}