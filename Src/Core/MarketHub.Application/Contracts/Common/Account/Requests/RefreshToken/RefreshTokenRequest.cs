namespace MarketHub.Application.Contracts.Common.Account.Requests.RefreshToken;

using MediatR;

public sealed record RefreshTokenRequest : IRequest<RefreshTokenResponse>;