namespace MarketHub.Domain.Exceptions.Accounts.Confirmations;

public sealed class EmailAlreadyConfirmedException : Exception
{
    public EmailAlreadyConfirmedException()
    {
    }

    public EmailAlreadyConfirmedException(string message)
        : base(message)
    {
    }

    public EmailAlreadyConfirmedException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
        
    }
}