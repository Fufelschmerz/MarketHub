namespace MarketHub.Domain.Services.Messaging.EmailMessaging;

using Entities.Messaging.EmailMessaging;
using Infrastructure.Persistence.Repositories;
using Specifications.Messaging.EmailMessaging;

public sealed class EmailMessageTemplateService : IEmailMessageTemplateService
{
    private readonly IDbRepository<EmailMessageTemplate> _emailMessageTemplateDbRepository;

    public EmailMessageTemplateService(IDbRepository<EmailMessageTemplate> emailMessageTemplateDbRepository)
    {
        _emailMessageTemplateDbRepository = emailMessageTemplateDbRepository ?? throw new ArgumentNullException(nameof(emailMessageTemplateDbRepository));
    }

    public async Task ActivateAsync(EmailMessageTemplate emailMessageTemplate,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplateByTypeActiveSpecification emailMessageTemplateByTypeActiveSpec = 
            new(emailMessageTemplate.EmailMessageTemplateType);

        EmailMessageTemplate? currentActiveEmailMessageTemplate = await _emailMessageTemplateDbRepository
            .SingleOrDefaultAsync(emailMessageTemplateByTypeActiveSpec,
                cancellationToken);

        if (currentActiveEmailMessageTemplate is not null)
        {
            currentActiveEmailMessageTemplate.SetIsActive(false);

            await _emailMessageTemplateDbRepository.UpdateAsync(currentActiveEmailMessageTemplate,
                cancellationToken);
        }

        emailMessageTemplate.SetIsActive(true);

        await _emailMessageTemplateDbRepository.UpdateAsync(emailMessageTemplate,
            cancellationToken);
    }
}