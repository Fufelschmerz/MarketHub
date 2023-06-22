namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.Activate;

using MediatR;

public sealed record EmailMessageTemplateActivateRequest(long Id) : IRequest;