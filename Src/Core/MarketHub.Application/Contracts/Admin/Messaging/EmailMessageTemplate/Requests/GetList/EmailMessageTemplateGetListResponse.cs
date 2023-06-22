namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetList;

using Dto;

public sealed record EmailMessageTemplateGetListResponse(IEnumerable<EmailMessageTemplateGetListDto> EmailMessageTemplates);