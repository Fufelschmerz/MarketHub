namespace MarketHub.Domain.Exceptions.Messaging.EmailMessaging;

public sealed class EmailMessageTemplateAlreadyExistsException : Exception
{
    public EmailMessageTemplateAlreadyExistsException()
    {
    }

    public EmailMessageTemplateAlreadyExistsException(string message)
        : base(message)
    {
    }

    public EmailMessageTemplateAlreadyExistsException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
        
    }
}