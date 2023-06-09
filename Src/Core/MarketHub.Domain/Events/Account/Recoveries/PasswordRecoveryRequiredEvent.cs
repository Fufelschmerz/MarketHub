namespace MarketHub.Domain.Events.Account.Recoveries;

using Entities.Accounts.Recoveries;
using Infrastructure.Domain.Events;

public sealed record PasswordRecoveryRequiredEvent(PasswordRecovery PasswordRecovery) : IDomainEvent;