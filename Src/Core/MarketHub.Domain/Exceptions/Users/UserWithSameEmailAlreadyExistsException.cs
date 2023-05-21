namespace MarketHub.Domain.Exceptions.Users;

public sealed class UserWithSameEmailAlreadyExistsException : Exception
{
    public UserWithSameEmailAlreadyExistsException()
    {
    }

    public UserWithSameEmailAlreadyExistsException(string message)
        : base(message)
    {
    }

    public UserWithSameEmailAlreadyExistsException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
    }
}