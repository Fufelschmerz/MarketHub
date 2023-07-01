namespace MarketHub.Application.Services.Queries.Messaging.EmailMessaging.Handlers;

using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MarketHub.Domain.Specifications.Messaging.EmailMessaging;

public sealed class FindEmailMessageTemplateByTypeActiveQueryHandler : IQueryHandler<FindEmailMessageTemplateByTypeActiveQuery,
        EmailMessageTemplate?>
{
    private readonly IDbRepository<EmailMessageTemplate> _emailMessageTemplateDbRepository;

    public FindEmailMessageTemplateByTypeActiveQueryHandler(IDbRepository<EmailMessageTemplate> emailMessageTemplateDbRepository)
    {
        _emailMessageTemplateDbRepository = emailMessageTemplateDbRepository ?? throw new ArgumentNullException(nameof(emailMessageTemplateDbRepository));
    }

    public Task<EmailMessageTemplate?> HandleAsync(FindEmailMessageTemplateByTypeActiveQuery query,
        CancellationToken cancellationToken = default)
    {
        EmailMessageTemplateByTypeActiveSpecification emailMessageTemplateByTypeActiveSpec =
            new(query.EmailMessageTemplateType);

        return _emailMessageTemplateDbRepository.SingleOrDefaultAsync(emailMessageTemplateByTypeActiveSpec,
            cancellationToken);
    }
}