namespace MarketHub.Application.Contracts.Common.Account.Requests.Logout;

using MediatR;
using Services.AuthenticationServices;

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