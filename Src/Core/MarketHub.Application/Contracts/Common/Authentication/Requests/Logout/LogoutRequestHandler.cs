namespace MarketHub.Application.Contracts.Common.Authentication.Requests.Logout;

using Services.AuthenticationServices;
using MediatR;

public sealed class LogoutRequestHandler : IRequestHandler<LogoutRequest>
{
    private readonly IAuthenticationService _authenticationService;

    public LogoutRequestHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }
    
    public async Task Handle(LogoutRequest request,
        CancellationToken cancellationToken)
    {
        await _authenticationService.LogoutAsync();
    }
}