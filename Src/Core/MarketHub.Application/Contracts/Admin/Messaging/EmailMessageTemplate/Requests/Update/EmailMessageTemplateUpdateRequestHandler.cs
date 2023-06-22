namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Update;

using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Infrastructure.Exceptions.Factories;
using Services.Commands;
using MarketHub.Application.Services.Queries.Common;
using MarketHub.Domain.Entities.Messaging.EmailMessaging;
using MediatR;

public sealed class EmailMessageTemplateUpdateRequestHandler : IRequestHandler<EmailMessageTemplateUpdateRequest>
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public EmailMessageTemplateUpdateRequestHandler(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
    }

    public async Task Handle(EmailMessageTemplateUpdateRequest request,
        CancellationToken cancellationToken)
    {
        EmailMessageTemplate emailMessageTemplate = await _queryDispatcher.FindByIdAsync<EmailMessageTemplate?>(
            request.Id,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(EmailMessageTemplate));

        emailMessageTemplate.SetEmailMessagingTemplateType(request.EmailMessageTemplateType);
        emailMessageTemplate.SetName(request.Name);
        emailMessageTemplate.SetDescription(request.Description);
        emailMessageTemplate.SetSubject(request.Subject);
        emailMessageTemplate.SetBody(request.Body);
        emailMessageTemplate.SetUpdatedDate();

        await _commandDispatcher.UpdateAsync(emailMessageTemplate,
            cancellationToken);
    }
}