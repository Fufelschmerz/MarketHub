namespace MarketHub.Domain.Services.Accounts;

using Confirmations;
using Entities.Accounts;
using Entities.Accounts.Confirmations;
using Entities.Users;
using Events.Account.Confirmations;
using Exceptions.Accounts;
using Exceptions.Accounts.Confirmations;
using Repositories.Accounts;
using Repositories.Accounts.Confirmations;
using Specifications.Accounts;
using Specifications.Accounts.Confirmations;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;

    private readonly IConfirmationTokenGenerator _confirmationTokenGenerator;

    public AccountService(IAccountRepository accountRepository,
        IEmailConfirmationRepository emailConfirmationRepository,
        IConfirmationTokenGenerator confirmationTokenGenerator)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _emailConfirmationRepository = emailConfirmationRepository ??
                                       throw new ArgumentNullException(nameof(emailConfirmationRepository));
        _confirmationTokenGenerator = confirmationTokenGenerator ??
                                      throw new ArgumentNullException(nameof(confirmationTokenGenerator));
    }

    public async Task<Account> BeginRegistrationAsync(User user,
        CancellationToken cancellationToken = default)
    {
        Account account = new(user);

        await CheckIsAccountWithUserExistsAsync(account,
            cancellationToken);

        await _accountRepository.AddAsync(account,
            cancellationToken);

        string emailConfirmationToken = _confirmationTokenGenerator.Create();

        EmailConfirmation emailConfirmation = new(account.User.Email,
            emailConfirmationToken);

        await _emailConfirmationRepository.AddAsync(emailConfirmation,
            cancellationToken);

        account.RaiseDomainEvent(new EmailConfirmationRequiredEvent(emailConfirmation));

        return account;
    }

    public async Task CompleteRegistrationAsync(Account account,
        string emailConfirmationToken,
        CancellationToken cancellationToken = default)
    {
        EmailConfirmationByTokenSpecification emailConfirmationByTokenSpec = new(emailConfirmationToken);

        EmailConfirmation emailConfirmation = await _emailConfirmationRepository.SingleOrDefaultAsync(
            emailConfirmationByTokenSpec,
            cancellationToken) ?? throw new InvalidConfirmationTokenException("Invalid email confirmation token");

        account.Confirm();

        await _emailConfirmationRepository.DeleteAsync(emailConfirmation,
            cancellationToken);
    }


    private async Task CheckIsAccountWithUserExistsAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        AccountByUserSpecification accountByUserSpec = new(account.User.Id);

        Account? existingAccount = await _accountRepository.SingleOrDefaultAsync(accountByUserSpec,
            cancellationToken);

        if (existingAccount != null && !Equals(account,
                existingAccount))
            throw new AccountWithSameUserAlreadyExistsException("Account with user already exists");
    }
}