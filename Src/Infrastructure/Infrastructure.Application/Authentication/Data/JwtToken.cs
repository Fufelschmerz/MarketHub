namespace Infrastructure.Application.Authentication.Data;

public sealed record JwtToken(string Jti,
    string Token);