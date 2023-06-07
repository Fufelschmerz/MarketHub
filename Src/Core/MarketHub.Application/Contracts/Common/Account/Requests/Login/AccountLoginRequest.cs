namespace MarketHub.Application.Contracts.Common.Account.Requests.Login;

using MediatR;

public sealed record AccountLoginRequest(string Email,
    string Password) : IRequest<AccountLoginResponse>;