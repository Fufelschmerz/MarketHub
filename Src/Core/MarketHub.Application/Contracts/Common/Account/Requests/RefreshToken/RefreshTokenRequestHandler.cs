namespace MarketHub.Application.Contracts.Common.Account.Requests.RefreshToken;

using global::Infrastructure.Application.Authentication.Data;
using MediatR;
using Services.AuthenticationServices;

public sealed class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenRequestHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }
    
    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        AccessToken accessToken = await _authenticationService.RefreshTokenAsync();

        return new RefreshTokenResponse(accessToken.Jwt);
    }
}