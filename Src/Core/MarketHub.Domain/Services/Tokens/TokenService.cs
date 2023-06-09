namespace MarketHub.Domain.Services.Tokens;

using System.Web;

public sealed class TokenService : ITokenService
{
    public string Create()
    {
        string token = Guid.NewGuid().ToString().Replace("-",
            string.Empty);

        return HttpUtility.UrlEncode(token);
    }
}