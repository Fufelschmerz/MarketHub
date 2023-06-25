namespace MarketHub.API.DI.Modules.Application;

using Autofac;
using Autofac.ConfiguredModules;
using MarketHub.Application.Services.Messaging.EmailMessaging.EmailSender.MailKit;
using MarketHub.Application.Services.Messaging.EmailMessaging.MessageFactory;

internal sealed class EmailMessagingModule : ConfiguredModule
{
    protected override void Load(ContainerBuilder builder)
    {
        MailKitOptions mailKitOptions = Configuration.GetSection("MailKitOptions")
            .Get<MailKitOptions>() ?? throw new ArgumentNullException(nameof(mailKitOptions));

        builder
            .RegisterType<MailKitEmailSenderService>()
            .AsImplementedInterfaces()
            .WithParameter("mailKitOptions", mailKitOptions)
            .InstancePerDependency();

        builder
            .RegisterType<EmailMessageFactory>()
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}