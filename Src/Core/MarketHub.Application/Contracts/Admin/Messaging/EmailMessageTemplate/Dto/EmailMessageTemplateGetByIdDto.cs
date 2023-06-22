namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Dto;

using Domain.Entities.Messaging.EmailMessaging.Enums;

public sealed record EmailMessageTemplateGetByIdDto(long Id,
    EmailMessageTemplateType EmailMessageTemplateType,
    string Name,
    string? Description,
    DateTime UpdatedAtUtc,
    string Subject,
    string Body,
    bool IsActive);