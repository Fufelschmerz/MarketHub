namespace MarketHub.Domain.Specifications.Messaging.EmailMessaging;

using Entities.Messaging.EmailMessaging;
using Entities.Messaging.EmailMessaging.Enums;
using Infrastructure.Specifications;

public sealed class EmailMessageTemplateByTypeActiveSpecification : Specification<EmailMessageTemplate>
{
    public EmailMessageTemplateByTypeActiveSpecification(EmailMessageTemplateType emailMessageTemplateType)
    {
        Query.Where(x => x.EmailMessageTemplateType == emailMessageTemplateType &&
                         x.IsActive);
    }
}