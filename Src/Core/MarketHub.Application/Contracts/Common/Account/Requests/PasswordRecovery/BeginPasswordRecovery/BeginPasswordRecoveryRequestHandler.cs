namespace MarketHub.Application.Contracts.Common.Account.Requests.PasswordRecovery.BeginPasswordRecovery;

using Domain.Entities.Users;
using Domain.Entities.Users.Recoveries;
using Domain.Services.Users.Recoveries;
using global::Infrastructure.Application.Services.Commands.Dispatchers;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using global::Infrastructure.Domain.Events;
using Infrastructure.Exceptions.Factories;
using MediatR;
using Services.Commands;
using Services.Queries.PasswordRecoveries;
using Services.Queries.Users;

public sealed class BeginPasswordRecoveryRequestHandler : IRequestHandler<BeginPasswordRecoveryRequest>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IPasswordRecoveryService _passwordRecoveryService;
    private readonly IMediator _mediator;

    public BeginPasswordRecoveryRequestHandler(IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        IPasswordRecoveryService passwordRecoveryService,
        IMediator mediator)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        _passwordRecoveryService = passwordRecoveryService ?? throw new ArgumentNullException(nameof(passwordRecoveryService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(BeginPasswordRecoveryRequest request,
        CancellationToken cancellationToken)
    {
        User user = await _queryDispatcher.FindUserByEmailAsync(request.Email,
            cancellationToken) ?? throw ApiExceptionFactory.ObjectNotFound(nameof(User));

        PasswordRecovery? existingPasswordRecovery = await _queryDispatcher.FindPasswordRecoveryByUserAsync(user,
            cancellationToken);

        if (existingPasswordRecovery is not null)
            await _commandDispatcher.DeleteAsync(existingPasswordRecovery,
                cancellationToken);
        
        await _passwordRecoveryService.CreateAsync(user,
            cancellationToken);

        foreach (IDomainEvent passwordRecoveryRequiredEvent in user.DomainEvents)
        {
            await _mediator.Publish(passwordRecoveryRequiredEvent,
                cancellationToken);
        }
    }
}