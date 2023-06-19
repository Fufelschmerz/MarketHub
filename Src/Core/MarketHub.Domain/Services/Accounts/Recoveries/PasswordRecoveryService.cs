namespace MarketHub.Domain.Services.Accounts.Recoveries;

using Entities.Accounts;
using Entities.Accounts.Recoveries;
using Events.Account.Recoveries;
using Exceptions.Tokens;
using Infrastructure.Persistence.Repositories;
using Specifications.Accounts.Recoveries;
using Tokens;

public sealed class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IRepository<PasswordRecovery> _passwordRecoveryRepository;
    private readonly ITokenService _tokenService;

    public PasswordRecoveryService(IRepository<PasswordRecovery> passwordRecoveryRepository,
        ITokenService tokenService)
    {
        _passwordRecoveryRepository = passwordRecoveryRepository ?? throw new ArgumentNullException(nameof(passwordRecoveryRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task CreateAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        string passwordRecoveryToken = _tokenService.Create();

        PasswordRecovery passwordRecovery = new(account,
            passwordRecoveryToken);

        await _passwordRecoveryRepository.AddAsync(passwordRecovery,
            cancellationToken);
        
        account.RaiseDomainEvent(new PasswordRecoveryRequiredEvent(passwordRecovery));
    }

    public async Task RecoverAsync(string token,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        PasswordRecoveryByTokenSpecification passwordRecoveryByTokenSpec = new(token);
        
        PasswordRecovery passwordRecovery = await _passwordRecoveryRepository.SingleOrDefaultAsync(
            passwordRecoveryByTokenSpec,
            cancellationToken) ?? throw new InvalidTokenException("Invalid password recovery token");
        
        passwordRecovery.Account.User.SetPassword(newPassword);
        
        await _passwordRecoveryRepository.DeleteAsync(passwordRecovery,
            cancellationToken);
    }
}