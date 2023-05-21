namespace MarketHub.Domain.Exceptions.Accounts.Confirmations;

public sealed class InvalidConfirmationTokenException : Exception
{
    public InvalidConfirmationTokenException()
    {
    }

    public InvalidConfirmationTokenException(string message)
        : base(message)
    {
    }

    public InvalidConfirmationTokenException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
    }
}