namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetById;

using MediatR;

public sealed record EmailMessageTemplateGetByIdRequest(long Id) : IRequest<EmailMessageTemplateGetByIdResponse>;