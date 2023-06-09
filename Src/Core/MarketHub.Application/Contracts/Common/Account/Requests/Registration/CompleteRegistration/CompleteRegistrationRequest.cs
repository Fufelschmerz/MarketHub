namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration.CompleteRegistration;

using MediatR;

public sealed record CompleteRegistrationRequest(string Token) : IRequest;