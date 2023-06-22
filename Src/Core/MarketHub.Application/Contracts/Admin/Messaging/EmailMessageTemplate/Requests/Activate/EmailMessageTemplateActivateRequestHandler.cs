namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Activate;

using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Infrastructure.Exceptions.Factories;
using MarketHub.Application.Services.Queries.Common;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MarketHub.Domain.Services.Messaging.EmailMessaging;
using MediatR;

public sealed class EmailMessageTemplateActivateRequestHandler : IRequestHandler<EmailMessageTemplateActivateRequest>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IEmailMessageTemplateService _emailMessageTemplateService;

    public EmailMessageTemplateActivateRequestHandler(IQueryDispatcher emailMessageTemplateQueryService,
        IEmailMessageTemplateService emailMessageTemplateService)
    {
        _queryDispatcher = emailMessageTemplateQueryService ?? throw new ArgumentNullException(nameof(emailMessageTemplateQueryService));
        _emailMessageTemplateService = emailMessageTemplateService ?? throw new ArgumentNullException(nameof(emailMessageTemplateService));
    }

    public async Task Handle(EmailMessageTemplateActivateRequest request,
        CancellationToken cancellationToken)
    {
        EmailMessageTemplate emailMessageTemplate = await _queryDispatcher.FindByIdAsync<EmailMessageTemplate?>(
            request.Id,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(EmailMessageTemplate));

        await _emailMessageTemplateService.ActivateAsync(emailMessageTemplate,
            cancellationToken);
    }
}