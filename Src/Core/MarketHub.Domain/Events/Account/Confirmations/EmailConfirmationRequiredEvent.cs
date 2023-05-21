namespace MarketHub.Domain.Events.Account.Confirmations;

using Entities.Accounts.Confirmations;
using Infrastructure.Domain.Events;

public sealed record EmailConfirmationRequiredEvent(EmailConfirmation EmailConfirmation) : IDomainEvent;