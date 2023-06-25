namespace MarketHub.Application.Contracts.Common.Account.Requests.EmailConfirmation.CompleteEmailConfirmation;

using MediatR;

public sealed record CompleteEmailConfirmationRequest(string Token) : IRequest;