namespace MarketHub.Application.Contracts.Common.Account.Requests.Login;

using global::Infrastructure.Application.Authentication.Data;
using Infrastructure.Exceptions.Factories;
using Services.AuthenticationServices;
using MediatR;

public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginRequestHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    public async Task<LoginResponse> Handle(LoginRequest request,
        CancellationToken cancellationToken)
    {
        JwtToken jwtToken = await _authenticationService.LoginAsync(request.Email,
            request.Password,
            cancellationToken) ?? throw ApiExceptionFactory.WrongCredentials;

        return new LoginResponse(jwtToken.Token);
    }
}