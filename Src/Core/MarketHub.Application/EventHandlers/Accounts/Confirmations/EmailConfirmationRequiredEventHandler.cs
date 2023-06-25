namespace MarketHub.Application.EventHandlers.Accounts.Confirmations;

using MarketHub.Domain.Events.Accounts.Confirmations;
using MediatR;
using Services.Messaging.EmailMessaging.EmailSender;
using Services.Messaging.EmailMessaging.MessageFactory;

public sealed class EmailConfirmationRequiredEventHandler : INotificationHandler<EmailConfirmationRequiredEvent>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailMessageFactory _emailMessageFactory;

    public EmailConfirmationRequiredEventHandler(IEmailSenderService emailSenderService,
        IEmailMessageFactory emailMessageFactory)
    {
        _emailSenderService = emailSenderService ?? throw new ArgumentNullException(nameof(emailSenderService));
        _emailMessageFactory = emailMessageFactory ?? throw new ArgumentNullException(nameof(emailMessageFactory));
    }
    
    public async Task Handle(EmailConfirmationRequiredEvent notification,
        CancellationToken cancellationToken)
    {
        (string subject, string body) emailConfirmationRequiredMessage = await _emailMessageFactory
            .CreateEmailConfirmationRequiredMessageAsync(notification.EmailConfirmation.Token, cancellationToken);

        await _emailSenderService.SendAsync(notification.EmailConfirmation.Account.User.Email,
            emailConfirmationRequiredMessage.subject,
            emailConfirmationRequiredMessage.body,
            cancellationToken);
    }
}