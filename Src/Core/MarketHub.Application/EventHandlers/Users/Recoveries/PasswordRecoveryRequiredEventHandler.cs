namespace MarketHub.Application.EventHandlers.Users.Recoveries;

using Services.Messaging.EmailMessaging.EmailSender;
using Services.Messaging.EmailMessaging.MessageFactory;
using MarketHub.Domain.Events.Users.Recoveries;
using MediatR;

public sealed class PasswordRecoveryRequiredEventHandler : INotificationHandler<PasswordRecoveryRequiredEvent>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailMessageFactory _emailMessageFactory;

    public PasswordRecoveryRequiredEventHandler(IEmailSenderService emailSenderService,
        IEmailMessageFactory emailMessageFactory)
    {
        _emailSenderService = emailSenderService ?? throw new ArgumentNullException(nameof(emailSenderService));
        _emailMessageFactory = emailMessageFactory ?? throw new ArgumentNullException(nameof(emailMessageFactory));
    }

    public async Task Handle(PasswordRecoveryRequiredEvent notification,
        CancellationToken cancellationToken)
    {
        (string subject, string body) passwordRecoveryMessage = await _emailMessageFactory
            .CreatePasswordRecoveryMessageAsync(notification.PasswordRecovery.Token, cancellationToken);

        await _emailSenderService.SendAsync(notification.PasswordRecovery.User.Email,
            passwordRecoveryMessage.subject,
            passwordRecoveryMessage.body,
            cancellationToken);
    }
}