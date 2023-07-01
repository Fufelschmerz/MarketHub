namespace MarketHub.Domain.Services.Accounts.Confirmations;

using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Events.Accounts.Confirmations;
using Exceptions.Tokens;
using Infrastructure.Persistence.Repositories;
using Specifications.Accounts.Confirmations;
using Tokens;

public sealed class EmailConfirmationService : IEmailConfirmationService
{
    private readonly IDbRepository<EmailConfirmation> _emailConfirmationDbRepository;
    private readonly ITokenService _tokenService;

    public EmailConfirmationService(IDbRepository<EmailConfirmation> emailConfirmationDbRepository,
        ITokenService tokenService)
    {
        _emailConfirmationDbRepository = emailConfirmationDbRepository ?? throw new ArgumentNullException(nameof(emailConfirmationDbRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task CreateAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        string token = _tokenService.Create();

        EmailConfirmation emailConfirmation = new(account,
            token);

        await _emailConfirmationDbRepository.AddAsync(emailConfirmation,
            cancellationToken);
        
        account.RaiseDomainEvent(new EmailConfirmationRequiredEvent(emailConfirmation));
    }

    public async Task ConfirmAsync(string token,
        CancellationToken cancellationToken = default)
    {
        EmailConfirmationByTokenSpecification emailConfirmationByTokenSpec = new(token);

        EmailConfirmation emailConfirmation = await _emailConfirmationDbRepository.SingleOrDefaultAsync(
            emailConfirmationByTokenSpec,
            cancellationToken) ?? throw new InvalidTokenException("Invalid email confirmation token");
        
        emailConfirmation.Account.ConfirmEmail();

        await _emailConfirmationDbRepository.DeleteAsync(emailConfirmation,
            cancellationToken);
    }
}