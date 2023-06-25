namespace MarketHub.Application.Services.Messaging.EmailMessaging.MessageFactory;

public interface IEmailMessageFactory
{
    Task<(string subject, string body)> CreateEmailConfirmationRequiredMessageAsync(string token,
        CancellationToken cancellationToken = default);

    Task<(string subject, string body)> CreatePasswordRecoveryMessageAsync(string token,
        CancellationToken cancellationToken = default);
}