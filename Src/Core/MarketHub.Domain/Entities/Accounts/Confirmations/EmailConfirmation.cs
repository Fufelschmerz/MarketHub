namespace MarketHub.Domain.Entities.Accounts.Confirmations;

using Infrastructure.Domain.Entities;

public sealed class EmailConfirmation : Entity
{
    private EmailConfirmation()
    {
    }

    public EmailConfirmation(string email,
        string token)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        Email = email;
        Token = token;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? CompletedAtUtc { get; private set; }

    public string Email { get; private set; }

    public string Token { get; private set; }

    public bool IsCompleted => CompletedAtUtc.HasValue;

    protected internal void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Already completed");

        CompletedAtUtc = DateTime.UtcNow;
    }
}