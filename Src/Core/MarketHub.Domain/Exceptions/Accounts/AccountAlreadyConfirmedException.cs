namespace MarketHub.Domain.Exceptions.Accounts;

public sealed class AccountAlreadyConfirmedException : Exception
{
    public AccountAlreadyConfirmedException()
    {
    }

    public AccountAlreadyConfirmedException(string message)
        : base(message)
    {
    }

    public AccountAlreadyConfirmedException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
        
    }
}