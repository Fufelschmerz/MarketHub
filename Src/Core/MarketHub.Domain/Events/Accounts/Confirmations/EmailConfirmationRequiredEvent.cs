namespace MarketHub.Domain.Events.Accounts.Confirmations;

using Infrastructure.Domain.Events;
using MarketHub.Domain.Entities.Accounts.Confirmations;

public sealed record EmailConfirmationRequiredEvent(EmailConfirmation EmailConfirmation) : IDomainEvent;