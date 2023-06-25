namespace MarketHub.Domain.Exceptions.Users.Recoveries;

public sealed class PasswordRecoveryExpiredException : Exception
{
    public PasswordRecoveryExpiredException()
    {
        
    }
    
    public PasswordRecoveryExpiredException(string message)
        : base(message)
    {
    }

    public PasswordRecoveryExpiredException(string message,
        Exception innerException)
        : base(message,
            innerException)
    {
    }
}