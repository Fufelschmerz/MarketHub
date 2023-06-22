namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetById;

using AutoMapper;
using Domain.Entities.Messaging.EmailMessaging;
using Dto;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Infrastructure.Exceptions.Factories;
using MediatR;
using Services.Queries.Common;

public sealed class EmailMessageTemplateGetByIdRequestHandler : IRequestHandler<EmailMessageTemplateGetByIdRequest,
    EmailMessageTemplateGetByIdResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public EmailMessageTemplateGetByIdRequestHandler(IQueryDispatcher queryDispatcher,
        IMapper mapper)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<EmailMessageTemplateGetByIdResponse> Handle(EmailMessageTemplateGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        EmailMessageTemplate emailMessageTemplate = await _queryDispatcher.FindByIdAsync<EmailMessageTemplate?>(
            request.Id,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(Messaging.EmailMessageTemplate));

        EmailMessageTemplateGetByIdDto dto = _mapper.Map<EmailMessageTemplateGetByIdDto>(emailMessageTemplate);

        return new EmailMessageTemplateGetByIdResponse(dto);
    }
}