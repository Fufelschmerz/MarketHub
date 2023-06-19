namespace MarketHub.Application.Contracts.Common.Authentication.Requests.Login;

using System.Security.Claims;
using global::Infrastructure.Application.Authentication.Data;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Infrastructure.Exceptions.Factories;
using Infrastructure.Identity.Claims.Factories;
using Services.AuthenticationServices;
using Domain.Entities.Users;
using MediatR;
using Services.Queries.Users;

public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IAuthenticationService _authenticationService;

    public LoginRequestHandler(IQueryDispatcher queryDispatcher,
        IAuthenticationService authenticationService)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    public async Task<LoginResponse> Handle(LoginRequest request,
        CancellationToken cancellationToken)
    {
        User? user = await _queryDispatcher.ExecuteAsync<FindUserByEmailQuery, User?>(
            new FindUserByEmailQuery(request.Email),
            cancellationToken);

        if (user is null || !user.Password.Check(request.Password))
            throw ApiExceptionFactory.WrongCredentials;

        Claim[] claims = ClaimFactory.CreateClaims(user).ToArray();

        AccessToken accessToken = await _authenticationService.LoginAsync(claims);

        return new LoginResponse(accessToken.Jwt);
    }
}