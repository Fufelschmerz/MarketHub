namespace Infrastructure.Application.Authentication.Data;

public sealed record AccessToken(string Jti,
    string Jwt,
    TimeSpan Expires,
    RefreshToken RefreshToken);