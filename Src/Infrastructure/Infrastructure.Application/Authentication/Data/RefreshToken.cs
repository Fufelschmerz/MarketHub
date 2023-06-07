namespace Infrastructure.Application.Authentication.Data;

public sealed record RefreshToken(string Token,
    DateTime CreatedAtUtc,
    TimeSpan Expires)
{
    public bool IsExpired() => DateTime.UtcNow < CreatedAtUtc.Add(Expires);
}