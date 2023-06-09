namespace MarketHub.Domain.Services.Accounts;

using Confirmations;
using Entities.Accounts;
using Exceptions.Accounts;
using Repositories.Accounts;
using Specifications.Accounts;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public AccountService(IAccountRepository accountRepository,
        IEmailConfirmationService emailConfirmationService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _emailConfirmationService = emailConfirmationService ?? throw new ArgumentNullException(nameof(emailConfirmationService));
    }

    public async Task RegistrationAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        await CheckIsAccountWithUserExistsAsync(account,
            cancellationToken);

        await _accountRepository.AddAsync(account,
            cancellationToken);

        await _emailConfirmationService.CreateAsync(account,
            cancellationToken);
    }
    
    private async Task CheckIsAccountWithUserExistsAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        AccountByUserSpecification accountByUserSpec = new(account.User.Id);

        Account? existingAccount = await _accountRepository.SingleOrDefaultAsync(accountByUserSpec,
            cancellationToken);

        if (existingAccount != null)
            throw new AccountWithSameUserAlreadyExistsException("Account with user already exists");
    }
}