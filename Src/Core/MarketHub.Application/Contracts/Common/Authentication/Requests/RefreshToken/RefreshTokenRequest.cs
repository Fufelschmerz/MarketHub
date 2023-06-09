namespace MarketHub.Application.Contracts.Common.Authentication.Requests.RefreshToken;

using MediatR;

public sealed record RefreshTokenRequest : IRequest<RefreshTokenResponse>;