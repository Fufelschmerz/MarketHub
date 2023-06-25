namespace MarketHub.Application.Services.Messaging.EmailMessaging.EmailSender;

public interface IEmailSenderService
{
    Task SendAsync(string addressee, string subject, string body,
        CancellationToken cancellationToken = default);
}