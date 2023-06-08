namespace MarketHub.Application.Contracts.Common.Account.Requests.Login;

using System.Security.Claims;
using Domain.Entities.Users;
using global::Infrastructure.Application.Authentication.Data;
using Infrastructure.Exceptions.Factories;
using Infrastructure.Identity.Claims.Factories;
using Services.AuthenticationServices;
using MediatR;
using Services.QueryServices.Users;

public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserQueryService _userQueryService;
    private readonly IAuthenticationService _authenticationService;

    public LoginRequestHandler(IUserQueryService userQueryService,
        IAuthenticationService authenticationService)
    {
        _userQueryService = userQueryService ?? throw new ArgumentNullException(nameof(userQueryService));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    public async Task<LoginResponse> Handle(LoginRequest request,
        CancellationToken cancellationToken)
    {
        User? user = await _userQueryService.FindByEmailAsync(request.Email,
            cancellationToken);

        if (user is null || !user.Password.Check(request.Password))
            throw ApiExceptionFactory.WrongCredentials;

        Claim[] claims = ClaimFactory.CreateClaims(user).ToArray();

        AccessToken accessToken = await _authenticationService.LoginAsync(claims);

        return new LoginResponse(accessToken.Jwt);
    }
}