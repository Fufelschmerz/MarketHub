namespace Infrastructure.Specifications.Exceptions;

public sealed class DuplicateOrderChainException : Exception
{
    private const string DefaultMessage = "The specification contains more than one Order chain";

    public DuplicateOrderChainException()
        : base(DefaultMessage)
    {
    }

    public DuplicateOrderChainException(Exception innerException)
        : base(DefaultMessage, innerException)
    {
    }
}