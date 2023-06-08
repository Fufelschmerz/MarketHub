namespace MarketHub.Application.Infrastructure.Cache;

public static class Keys
{
    public static string GetRefreshTokenKey(string jti) => $"tokens:refresh:{jti}";

    public static string GetAccessTokenKey(string jti) => $"tokens:jwt:{jti}";
}