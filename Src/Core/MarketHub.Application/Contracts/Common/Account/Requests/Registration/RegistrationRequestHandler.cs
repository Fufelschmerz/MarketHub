namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration;

using global::Infrastructure.Application.Services.Queries.Dispatchers;
using global::Infrastructure.Domain.Events;
using Services.Queries.Roles;
using Domain.Entities.Accounts;
using Domain.Entities.Users;
using Domain.Entities.Users.Roles;
using MarketHub.Domain.Services.Accounts;
using MarketHub.Domain.Services.Users;
using MediatR;

public sealed class RegistrationRequestHandler : IRequestHandler<RegistrationRequest>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    public RegistrationRequestHandler(IQueryDispatcher queryDispatcher,
        IUserService userService,
        IAccountService accountService,
        IMediator mediator)
    {
        _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(RegistrationRequest request,
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