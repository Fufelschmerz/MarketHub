namespace MarketHub.Domain.Services.Accounts;

using Confirmations;
using Entities.Accounts;
using Exceptions.Accounts;
using Infrastructure.Persistence.Repositories;
using Specifications.Accounts;

public sealed class AccountService : IAccountService
{
    private readonly IDbRepository<Account> _accountDbRepository;

    public AccountService(IDbRepository<Account> accountDbRepository)
    {
        _accountDbRepository = accountDbRepository ?? throw new ArgumentNullException(nameof(accountDbRepository));
    }

    public async Task RegistrationAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        await CheckIsAccountWithUserExistsAsync(account,
            cancellationToken);

        await _accountDbRepository.AddAsync(account,
            cancellationToken);
    }
    
    private async Task CheckIsAccountWithUserExistsAsync(Account account,
        CancellationToken cancellationToken = default)
    {
        AccountByUserSpecification accountByUserSpec = new(account.User.Id);

        Account? existingAccount = await _accountDbRepository.SingleOrDefaultAsync(accountByUserSpec,
            cancellationToken);

        if (existingAccount is not null)
            throw new AccountWithSameUserAlreadyExistsException("Account with user already exists");
    }
}