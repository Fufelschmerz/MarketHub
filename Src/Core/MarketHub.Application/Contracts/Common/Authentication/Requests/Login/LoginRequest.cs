namespace MarketHub.Application.Contracts.Common.Authentication.Requests.Login;

using MediatR;

public sealed record LoginRequest(string Email,
    string Password) : IRequest<LoginResponse>;