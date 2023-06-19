namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;

using Domain.Entities.Users;
using Domain.Services.Accounts.Recoveries;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using Infrastructure.Exceptions.Factories;
using MediatR;
using Services.Queries.Users;

public sealed class BeginPasswordRecoveryRequestHandler : IRequestHandler<BeginPasswordRecoveryRequest>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IPasswordRecoveryService _passwordRecoveryService;

    public BeginPasswordRecoveryRequestHandler(IQueryDispatcher queryDispatcher,
        IPasswordRecoveryService passwordRecoveryService)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _passwordRecoveryService = passwordRecoveryService ?? throw new ArgumentNullException(nameof(passwordRecoveryService));
    }

    public async Task Handle(BeginPasswordRecoveryRequest request,
        CancellationToken cancellationToken)
    {
        User user = await _queryDispatcher.FindUserByEmailAsync(request.Email,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(User));

        await _passwordRecoveryService.CreateAsync(user.Account,
            cancellationToken);
    }
}