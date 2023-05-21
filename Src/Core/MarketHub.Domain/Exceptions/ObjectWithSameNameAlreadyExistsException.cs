namespace MarketHub.Domain.Exceptions;

public sealed class ObjectWithSameNameAlreadyExistsException : Exception
{
    public ObjectWithSameNameAlreadyExistsException()
    {
    }

    public ObjectWithSameNameAlreadyExistsException(string message)
        : base(message)
    {
    }

    public ObjectWithSameNameAlreadyExistsException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
    }
}