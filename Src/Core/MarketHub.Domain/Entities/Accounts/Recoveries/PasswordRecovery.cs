namespace MarketHub.Domain.Entities.Accounts.Recoveries;

using Infrastructure.Domain.Entities;

public sealed class PasswordRecovery : Entity
{
    private PasswordRecovery()
    {
    }

    public PasswordRecovery(Account account,
        string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        CreatedAtUtc = DateTime.UtcNow;
        Account = account ?? throw new ArgumentNullException(nameof(account));
        Token = token;
    }

    public DateTime CreatedAtUtc { get; private set; }

    public long AccountId { get; private set; }

    public Account Account { get; private set; }

    public string Token { get; private set; }
}