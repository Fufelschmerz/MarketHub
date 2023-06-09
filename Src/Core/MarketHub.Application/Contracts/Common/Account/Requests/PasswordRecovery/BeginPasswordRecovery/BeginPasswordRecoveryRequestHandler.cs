namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;

using Domain.Entities.Users;
using Domain.Services.Accounts.Recoveries;
using Infrastructure.Exceptions.Factories;
using MediatR;
using Services.QueryServices.Users;

public sealed class BeginPasswordRecoveryRequestHandler : IRequestHandler<BeginPasswordRecoveryRequest>
{
    private readonly IUserQueryService _userQueryService;
    private readonly IPasswordRecoveryService _passwordRecoveryService;

    public BeginPasswordRecoveryRequestHandler(IUserQueryService userQueryService,
        IPasswordRecoveryService passwordRecoveryService)
    {
        _userQueryService = userQueryService ?? throw new ArgumentNullException(nameof(userQueryService));
        _passwordRecoveryService = passwordRecoveryService ?? throw new ArgumentNullException(nameof(passwordRecoveryService));
    }
    
    public async Task Handle(BeginPasswordRecoveryRequest request,
        CancellationToken cancellationToken)
    {
        User user = await _userQueryService.FindByEmailAsync(request.Email,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(User));

        await _passwordRecoveryService.CreateAsync(user.Account, 
            cancellationToken);
    }
}