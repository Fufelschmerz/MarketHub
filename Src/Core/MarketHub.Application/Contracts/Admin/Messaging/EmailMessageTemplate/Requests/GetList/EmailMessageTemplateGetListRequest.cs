namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetList;

using MediatR;

public sealed record EmailMessageTemplateGetListRequest : IRequest<EmailMessageTemplateGetListResponse>;