namespace MarketHub.Application.Services.Messaging.EmailMessaging.EmailSender.MailKit;

using global::MailKit.Net.Smtp;
using global::MailKit.Security;
using MimeKit;

public sealed class MailKitEmailSenderService : IEmailSenderService
{
    private readonly MailKitOptions _mailKitOptions;

    public MailKitEmailSenderService(MailKitOptions mailKitOptions)
    {
        _mailKitOptions = mailKitOptions ?? throw new ArgumentNullException(nameof(mailKitOptions));
    }

    public async Task SendAsync(string addressee,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        MimeMessage message = new();

        message.From.Add(MailboxAddress.Parse(_mailKitOptions.From));
        message.To.Add(MailboxAddress.Parse(addressee));

        message.Subject = subject;

        BodyBuilder bodyBuilder = new()
        {
            HtmlBody = body
        };

        message.Body = bodyBuilder.ToMessageBody();

        using SmtpClient client = new();
        
        await client.ConnectAsync(
            _mailKitOptions.Host,
            _mailKitOptions.Port,
            _mailKitOptions.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None,
            cancellationToken);

        if (_mailKitOptions.NeedAuthentication)
        {
            await client.AuthenticateAsync(
                _mailKitOptions.Login,
                _mailKitOptions.Password,
                cancellationToken);
        }

        await client.SendAsync(message,
            cancellationToken);

        await client.DisconnectAsync(true,
            cancellationToken);
    }
}