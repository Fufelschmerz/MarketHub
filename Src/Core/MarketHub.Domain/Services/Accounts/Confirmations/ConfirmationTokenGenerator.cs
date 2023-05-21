namespace MarketHub.Domain.Services.Accounts.Confirmations;

using System.Web;

public sealed class ConfirmationTokenGenerator : IConfirmationTokenGenerator
{
    public string Create()
    {
        string token = Guid.NewGuid().ToString().Replace("-",
            string.Empty);

        return HttpUtility.UrlEncode(token);
    }
}