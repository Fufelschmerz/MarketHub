namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.CompletePasswordRecovery;

using MediatR;

public sealed record CompletePasswordRecoveryRequest(string Token,
    string NewPassword) : IRequest;