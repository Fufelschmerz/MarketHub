namespace MarketHub.Application.Services.Queries.Messaging.EmailMessaging.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MarketHub.Domain.Specifications.Messaging.EmailMessaging;

public sealed class FindEmailMessageTemplateByTypeActiveQueryHandler : IQueryHandler<FindEmailMessageTemplateByTypeActiveQuery,
        EmailMessageTemplate?>
{
    private readonly IRepository<EmailMessageTemplate> _emailMessageTemplateRepository;

    public FindEmailMessageTemplateByTypeActiveQueryHandler(IRepository<EmailMessageTemplate> emailMessageTemplateRepository)
    {
        _emailMessageTemplateRepository = emailMessageTemplateRepository ?? throw new ArgumentNullException(nameof(emailMessageTemplateRepository));
    }

    public Task<EmailMessageTemplate?> HandleAsync(FindEmailMessageTemplateByTypeActiveQuery query,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplateByTypeActiveSpecification emailMessageTemplateByTypeActiveSpec =
            new(query.EmailMessageTemplateType);

        return _emailMessageTemplateRepository.SingleOrDefaultAsync(emailMessageTemplateByTypeActiveSpec,
            cancellationToken);
    }
}