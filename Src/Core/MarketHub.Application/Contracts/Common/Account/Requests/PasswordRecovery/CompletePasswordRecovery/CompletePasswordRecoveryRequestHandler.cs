namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.CompletePasswordRecovery;

using Domain.Services.Accounts.Recoveries;
using MediatR;

public sealed record CompletePasswordRecoveryRequestHandler : IRequestHandler<CompletePasswordRecoveryRequest>
{
    private readonly IPasswordRecoveryService _passwordRecoveryService;

    public CompletePasswordRecoveryRequestHandler(IPasswordRecoveryService passwordRecoveryService)
    {
        _passwordRecoveryService = passwordRecoveryService ?? throw new ArgumentNullException(nameof(passwordRecoveryService));
    }
    
    public async Task Handle(CompletePasswordRecoveryRequest request,
        CancellationToken cancellationToken)
    {
        await _passwordRecoveryService.RecoverAsync(request.Token,
            request.NewPassword,
            cancellationToken);
    }
}