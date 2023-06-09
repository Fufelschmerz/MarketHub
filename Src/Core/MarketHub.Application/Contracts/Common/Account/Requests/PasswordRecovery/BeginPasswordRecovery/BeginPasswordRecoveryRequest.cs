namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;

using MediatR;

public sealed record BeginPasswordRecoveryRequest(string Email) : IRequest;