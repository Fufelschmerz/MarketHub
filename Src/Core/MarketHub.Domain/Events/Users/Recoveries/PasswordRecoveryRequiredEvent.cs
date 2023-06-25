namespace MarketHub.Domain.Events.Users.Recoveries;

using Infrastructure.Domain.Events;
using MarketHub.Domain.Entities.Users.Recoveries;

public sealed record PasswordRecoveryRequiredEvent(PasswordRecovery PasswordRecovery) : IDomainEvent;