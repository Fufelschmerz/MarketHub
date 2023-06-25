namespace MarketHub.Application.Contracts.Common.Account.Requests.EmailConfirmation.CompleteEmailConfirmation;

using Domain.Services.Accounts.Confirmations;
using MediatR;

public sealed class CompleteEmailConfirmationRequestHandler : IRequestHandler<CompleteEmailConfirmationRequest>
{
    private readonly IEmailConfirmationService _emailConfirmationService;

    public CompleteEmailConfirmationRequestHandler(IEmailConfirmationService emailConfirmationService)
    {
        _emailConfirmationService = emailConfirmationService ?? throw new ArgumentNullException(nameof(emailConfirmationService));
    }
    
    public async Task Handle(CompleteEmailConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        await _emailConfirmationService.ConfirmAsync(request.Token,
            cancellationToken);
    }
}