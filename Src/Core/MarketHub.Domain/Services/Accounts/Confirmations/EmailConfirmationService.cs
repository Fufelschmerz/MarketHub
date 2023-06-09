namespace MarketHub.Domain.Services.Accounts.Confirmations;

using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Events.Account.Confirmations;
using Exceptions.Tokens;
using Repositories.Accounts.Confirmations;
using Specifications.Accounts.Confirmations;
using Tokens;

public sealed class EmailConfirmationService : IEmailConfirmationService
{
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;
    private readonly ITokenService _tokenService;

    public EmailConfirmationService(IEmailConfirmationRepository emailConfirmationRepository,
        ITokenService tokenService)
    {
        _emailConfirmationRepository = emailConfirmationRepository ?? throw new ArgumentNullException(nameof(emailConfirmationRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task CreateAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        string token = _tokenService.Create();

        EmailConfirmation emailConfirmation = new(account,
            token);

        await _emailConfirmationRepository.AddAsync(emailConfirmation,
            cancellationToken);
        
        account.RaiseDomainEvent(new EmailConfirmationRequiredEvent(emailConfirmation));
    }

    public async Task ConfirmAsync(string token,
        CancellationToken cancellationToken = default)
    {
        EmailConfirmationByTokenSpecification emailConfirmationByTokenSpec = new(token);

        EmailConfirmation emailConfirmation = await _emailConfirmationRepository.SingleOrDefaultAsync(
            emailConfirmationByTokenSpec,
            cancellationToken) ?? throw new InvalidTokenException("Invalid email confirmation token");
        
        emailConfirmation.Account.ConfirmEmail();

        await _emailConfirmationRepository.DeleteAsync(emailConfirmation,
            cancellationToken);
    }
}