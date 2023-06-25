namespace MarketHub.Application.Services.Messaging.EmailMessaging.EmailSender.MailKit;

public sealed class MailKitOptions
{
    public string Host { get; set; }

    public int Port { get; set; }

    public bool UseSsl { get; set; }

    public bool NeedAuthentication { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string From { get; set; }
}