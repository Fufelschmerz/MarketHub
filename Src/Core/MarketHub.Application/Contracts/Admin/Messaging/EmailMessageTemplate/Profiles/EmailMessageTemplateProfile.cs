namespace MarketHub.Application.Contracts.Admin.Messaging.EmailMessageTemplate.Profiles;

using AutoMapper;
using Domain.Entities.Messaging.EmailMessaging;
using Dto;

public sealed class EmailMessageTemplateProfile : Profile
{
    public EmailMessageTemplateProfile()
    {
        CreateMap<EmailMessageTemplate, EmailMessageTemplateGetByIdDto>();
        CreateMap<EmailMessageTemplate, EmailMessageTemplateGetListDto>();
    }
}