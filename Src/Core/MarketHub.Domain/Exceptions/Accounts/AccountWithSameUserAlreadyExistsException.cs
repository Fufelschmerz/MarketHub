namespace MarketHub.Domain.Exceptions.Accounts;

public sealed class AccountWithSameUserAlreadyExistsException : Exception
{
    public AccountWithSameUserAlreadyExistsException()
    {
    }

    public AccountWithSameUserAlreadyExistsException(string message)
        : base(message)
    {
    }

    public AccountWithSameUserAlreadyExistsException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
    }
}