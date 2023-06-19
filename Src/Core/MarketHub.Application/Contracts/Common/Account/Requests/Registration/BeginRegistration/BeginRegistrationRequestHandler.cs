namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration.BeginRegistration;

using global::Infrastructure.Domain.Events;
using Domain.Entities.Accounts;
using Domain.Entities.Users;
using Domain.Entities.Users.Roles;
using global::Infrastructure.Application.Services.Queries.Dispatchers;
using MarketHub.Domain.Services.Accounts;
using MarketHub.Domain.Services.Users;
using MediatR;
using Services.Queries.Roles;

public sealed class BeginRegistrationRequestHandler : IRequestHandler<BeginRegistrationRequest>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    public BeginRegistrationRequestHandler(IQueryDispatcher queryDispatcher,
        IUserService userService,
        IAccountService accountService,
        IMediator mediator)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(BeginRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Role> roles = await _queryDispatcher.FindRolesByTypesAsync(request.RoleTypes,
            cancellationToken);

        User user = new(request.Name,
            request.Email,
            request.Password,
            roles);

        await _userService.CreateAsync(user,
            cancellationToken);

        Account account = new(user);

        await _accountService.RegistrationAsync(account,
            cancellationToken);

        foreach (IDomainEvent domainEvent in account.DomainEvents)
        {
            await _mediator.Publish(domainEvent,
                cancellationToken);
        }
    }
}