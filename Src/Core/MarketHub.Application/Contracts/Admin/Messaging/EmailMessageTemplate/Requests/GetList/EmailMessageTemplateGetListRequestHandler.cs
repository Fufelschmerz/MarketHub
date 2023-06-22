namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Requests.GetList;

using AutoMapper;
using Domain.Entities.Messaging.EmailMessaging;
using Dto;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using MediatR;
using Services.Queries.Common;

public sealed class EmailMessageTemplateGetListRequestHandler : IRequestHandler<EmailMessageTemplateGetListRequest,
    EmailMessageTemplateGetListResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public EmailMessageTemplateGetListRequestHandler(IQueryDispatcher queryDispatcher,
        IMapper mapper)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<EmailMessageTemplateGetListResponse> Handle(EmailMessageTemplateGetListRequest request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<EmailMessageTemplate> emailMessageTemplates = await _queryDispatcher
            .FindAllAsync<EmailMessageTemplate>(cancellationToken);

        IEnumerable<EmailMessageTemplateGetListDto> dtos = _mapper
            .Map<IEnumerable<EmailMessageTemplateGetListDto>>(emailMessageTemplates);

        return new EmailMessageTemplateGetListResponse(dtos);
    }
}