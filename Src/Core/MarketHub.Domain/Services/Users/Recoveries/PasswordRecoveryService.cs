namespace MarketHub.Domain.Services.Users.Recoveries;

using Entities.Users;
using Events.Users.Recoveries;
using Exceptions.Users.Recoveries;
using Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Users.Recoveries;
using MarketHub.Domain.Exceptions.Tokens;
using Specifications.Users.Recoveries;
using Tokens;

public sealed class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IDbRepository<PasswordRecovery> _passwordRecoveryDbRepository;
    private readonly ITokenService _tokenService;

    public PasswordRecoveryService(IDbRepository<PasswordRecovery> passwordRecoveryDbRepository,
        ITokenService tokenService)
    {
        _passwordRecoveryDbRepository = passwordRecoveryDbRepository ?? throw new ArgumentNullException(nameof(passwordRecoveryDbRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task CreateAsync(User user,
        CancellationToken cancellationToken = default)
    {
        string passwordRecoveryToken = _tokenService.Create();

        PasswordRecovery passwordRecovery = new(user,
            passwordRecoveryToken);

        await _passwordRecoveryDbRepository.AddAsync(passwordRecovery,
            cancellationToken);
        
        user.RaiseDomainEvent(new PasswordRecoveryRequiredEvent(passwordRecovery));
    }

    public async Task RecoverAsync(string token,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        PasswordRecoveryByTokenSpecification passwordRecoveryByTokenSpec = new(token);
        
        PasswordRecovery passwordRecovery = await _passwordRecoveryDbRepository.SingleOrDefaultAsync(
            passwordRecoveryByTokenSpec,
            cancellationToken) ?? throw new InvalidTokenException("Invalid password recovery token");

        if (passwordRecovery.IsExpired)
            throw new PasswordRecoveryExpiredException("The password recovery period has expired");
        
        passwordRecovery.User.SetPassword(newPassword);
        
        await _passwordRecoveryDbRepository.DeleteAsync(passwordRecovery,
            cancellationToken);
    }
}