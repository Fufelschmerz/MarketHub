namespace MarketHub.Domain.Entities.Users.Recoveries;

using Infrastructure.Domain.Entities;

public sealed class PasswordRecovery : Entity
{
    private static readonly TimeSpan ValidityPeriod = TimeSpan.FromDays(1);
    
    private PasswordRecovery()
    {
    }

    public PasswordRecovery(User user,
        string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        UpdatedAtUtc = DateTime.UtcNow;
        User = user ?? throw new ArgumentNullException(nameof(user));
        Token = token;
    }

    public DateTime UpdatedAtUtc { get; private set; }

    public long UserId { get; private set; }

    public User User { get; private set; }

    public string Token { get; private set; }
    
    public DateTime ValidThroughUtc => UpdatedAtUtc.Add(ValidityPeriod);
    
    public bool IsExpired => ValidThroughUtc < DateTime.UtcNow;
}