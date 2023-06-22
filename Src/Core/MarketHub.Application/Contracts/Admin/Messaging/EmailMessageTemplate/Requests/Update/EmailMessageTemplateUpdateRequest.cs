namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Update;

using MarketHub.Domain.Entities.Messaging.EmailMessaging.Enums;
using MediatR;

public sealed record EmailMessageTemplateUpdateRequest(long Id,
    EmailMessageTemplateType EmailMessageTemplateType,
    string Name,
    string? Description,
    string Subject,
    string Body) : IRequest;