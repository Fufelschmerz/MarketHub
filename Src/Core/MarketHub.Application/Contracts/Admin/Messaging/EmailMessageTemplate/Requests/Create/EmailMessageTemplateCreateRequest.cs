namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Create;

using MarketHub.Domain.Entities.Messaging.EmailMessaging.Enums;
using MediatR;

public sealed record EmailMessageTemplateCreateRequest(EmailMessageTemplateType EmailMessageTemplateType,
    string Name,
    string? Description,
    string Subject,
    string Body) : IRequest<EmailMessageTemplateCreateResponse>;