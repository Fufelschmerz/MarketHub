namespace MarketHub.Domain.Entities.Messaging.EmailMessaging;

using Enums;
using Infrastructure.Domain.Entities;

public sealed class EmailMessageTemplate : Entity
{
    private EmailMessageTemplate()
    {
    }

    public EmailMessageTemplate(EmailMessageTemplateType emailMessageTemplateType,
        string name,
        string? description,
        string subject,
        string body)
    {
        SetEmailMessagingTemplateType(emailMessageTemplateType);
        SetName(name);
        SetDescription(description);
        SetSubject(subject);
        SetBody(body);
        SetUpdatedDate();
    }

    public EmailMessageTemplateType EmailMessageTemplateType { get; private set; }
    
    public string Name { get; private set; }
    
    public string? Description { get; private set; }
    
    public DateTime UpdatedAtUtc { get; private set; }

    public string Subject { get; private set; }
    
    public string Body { get; private set; }

    public bool IsActive { get; private set; }

    public void SetEmailMessagingTemplateType(EmailMessageTemplateType emailMessageTemplateType)
    {
        EmailMessageTemplateType = emailMessageTemplateType;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
    }
    
    public void SetDescription(string? description)
    {
        Description = description;
    }
    
    public void SetSubject(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentNullException(nameof(subject));

        Subject = subject;
    }

    public void SetBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
            throw new ArgumentNullException(nameof(body));

        Body = body;
    }

    public void SetUpdatedDate()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
}