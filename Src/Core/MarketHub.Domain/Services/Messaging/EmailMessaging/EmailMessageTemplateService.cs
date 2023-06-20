namespace MarketHub.Domain.Services.Messaging.EmailMessaging;

using Entities.Messaging.EmailMessaging;
using Infrastructure.Persistence.Repositories;
using Specifications.Messaging.EmailMessaging;

public sealed class EmailMessageTemplateService : IEmailMessageTemplateService
{
    private readonly IRepository<EmailMessageTemplate> _emailMessageTemplateRepository;

    public EmailMessageTemplateService(IRepository<EmailMessageTemplate> emailMessageTemplateRepository)
    {
        _emailMessageTemplateRepository = emailMessageTemplateRepository ?? throw new ArgumentNullException(nameof(emailMessageTemplateRepository));
    }

    public async Task ActivateAsync(EmailMessageTemplate emailMessageTemplate,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplateByTypeActiveSpecification emailMessageTemplateByTypeActiveSpec = 
            new(emailMessageTemplate.EmailMessageTemplateType);

        EmailMessageTemplate? currentActiveEmailMessageTemplate = await _emailMessageTemplateRepository
            .SingleOrDefaultAsync(emailMessageTemplateByTypeActiveSpec,
                cancellationToken);

        if (currentActiveEmailMessageTemplate is not null)
        {
            currentActiveEmailMessageTemplate.SetIsActive(false);

            await _emailMessageTemplateRepository.UpdateAsync(currentActiveEmailMessageTemplate,
                cancellationToken);
        }

        emailMessageTemplate.SetIsActive(true);

        await _emailMessageTemplateRepository.UpdateAsync(emailMessageTemplate,
            cancellationToken);
    }
}