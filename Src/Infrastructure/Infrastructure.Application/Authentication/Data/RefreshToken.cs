namespace Infrastructure.Application.Authentication.Data;

public sealed record RefreshTokenDto(string Token,
    DateTime CreatedAtUtc,
    TimeSpan Expires)
{
    public bool IsExpired() => DateTime.UtcNow < CreatedAtUtc.Add(Expires);
}