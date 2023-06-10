namespace MarketHub.Application.Contracts.Common.Account.Requests.Registration.BeginRegistration;

using global::Infrastructure.Domain.Events;
using Domain.Entities.Accounts;
using Domain.Entities.Users;
using Domain.Entities.Users.Roles;
using MarketHub.Domain.Services.Accounts;
using MarketHub.Domain.Services.Users;
using MediatR;
using Services.QueryServices.Users.Roles;

public sealed class BeginRegistrationRequestHandler : IRequestHandler<BeginRegistrationRequest>
{
    private readonly IRoleQueryService _roleQueryService;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    public BeginRegistrationRequestHandler(IRoleQueryService roleQueryService,
        IUserService userService,
        IAccountService accountService,
        IMediator mediator)
    {
        _roleQueryService = roleQueryService ?? throw new ArgumentNullException(nameof(roleQueryService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(BeginRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Role> roles = await _roleQueryService.FindRolesByTypesAsync(request.RoleTypes,
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