namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration.CompleteRegistration;

using Domain.Services.Accounts.Confirmations;
using MediatR;

public sealed class CompleteRegistrationRequestHandler : IRequestHandler<CompleteRegistrationRequest>
{
    private readonly IEmailConfirmationService _emailConfirmationService;
    
    public CompleteRegistrationRequestHandler(IEmailConfirmationService emailConfirmationService)
    {
        _emailConfirmationService = emailConfirmationService ?? throw new ArgumentNullException(nameof(emailConfirmationService));
    }

    public async Task Handle(CompleteRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        await _emailConfirmationService.ConfirmAsync(request.Token,
            cancellationToken);
    }
}