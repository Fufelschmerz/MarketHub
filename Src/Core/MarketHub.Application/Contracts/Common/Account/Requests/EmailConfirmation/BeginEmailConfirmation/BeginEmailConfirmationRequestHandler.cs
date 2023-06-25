namespace MarketHub.Application.Contracts.Common.Account.Requests.EmailConfirmation.BeginEmailConfirmation;

using Domain.Entities.Accounts.Confirmations;
using Domain.Entities.Users;
using Domain.Services.Accounts.Confirmations;
using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using global::Infrastructure.Domain.Events;
using MediatR;
using Services.AuthorizationServices;
using Services.Commands;
using Services.Queries.Confirmations.EmailConfirmation;

public sealed class BeginEmailConfirmationRequestHandler : IRequestHandler<BeginEmailConfirmationRequest>
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IAuthorizationService  _authorizationService;
    private readonly IEmailConfirmationService _emailConfirmationService;
    private readonly IMediator _mediator;

    public BeginEmailConfirmationRequestHandler(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        IAuthorizationService authorizationService,
        IEmailConfirmationService emailConfirmationService,
        IMediator mediator)
    {
        _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        _emailConfirmationService = emailConfirmationService ?? throw new ArgumentNullException(nameof(emailConfirmationService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task Handle(BeginEmailConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        User user = await _authorizationService.GetCurrentUserAsync(cancellationToken);

        EmailConfirmation? existingEmailConfirmation = await _queryDispatcher.FindEmailConfirmationByAccountAsync(
            user.Account,
            cancellationToken);
        
        if (existingEmailConfirmation is not null)
            await _commandDispatcher.DeleteAsync(existingEmailConfirmation,
                cancellationToken);
        
        await _emailConfirmationService.CreateAsync(user.Account,
            cancellationToken);
        
        foreach (IDomainEvent emailConfirmationRequiredEvent in user.Account.DomainEvents)
        {
            await _mediator.Publish(emailConfirmationRequiredEvent,
                cancellationToken);
        }
    }
}