namespace Infrastructure.Application.Exceptions;

public sealed class ApiException : Exception
{
    public ApiException(int extensionCode)
    {
        ExtensionCode = extensionCode;
    }

    public ApiException(int extensionCode,
        string message)
        : base(message)
    {
        ExtensionCode = extensionCode;
    }

    public ApiException(int extensionCode,
        string message,
        Exception innerException)
        : base(message,
            innerException)
    {
        ExtensionCode = extensionCode;
    }

    public int ExtensionCode { get; }
}