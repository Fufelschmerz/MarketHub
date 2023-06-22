namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Create;

using global::Infrastructure.Application.Services.Commands.Dispatchers;
using Services.Commands;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MediatR;

public sealed class EmailMessageTemplateCreateRequestHandler : IRequestHandler<EmailMessageTemplateCreateRequest,
    EmailMessageTemplateCreateResponse>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public EmailMessageTemplateCreateRequestHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
    }
    
    public async Task<EmailMessageTemplateCreateResponse> Handle(EmailMessageTemplateCreateRequest request,
        CancellationToken cancellationToken)
    {
        EmailMessageTemplate emailMessageTemplate = new(request.EmailMessageTemplateType,
            request.Name,
            request.Description,
            request.Subject,
            request.Body);

        await _commandDispatcher.CreateAsync(emailMessageTemplate,
            cancellationToken);

        return new EmailMessageTemplateCreateResponse(emailMessageTemplate.Id);
    }
}