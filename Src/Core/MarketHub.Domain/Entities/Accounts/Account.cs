namespace MarketHub.Domain.Entities.Accounts;

using Exceptions.Accounts.Confirmations;
using Infrastructure.Domain.Entities;
using Users;

public sealed class Account : Entity
{
    private Account()
    {
    }

    public Account(User user,
        decimal balance = 0)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        
        SetBalance(balance);
    }

    public long UserId { get; private set; }

    public User User { get; private set; }

    public decimal Balance { get; private set; }

    public bool IsEmailConfirmed { get; private set; }
    
    
    public  void ConfirmEmail()
    {
        if (IsEmailConfirmed)
            throw new EmailAlreadyConfirmedException("Email is already confirmed");

        IsEmailConfirmed = true;
    }
    
    public  void SetBalance(decimal balance)
    {
        if (balance < 0m)
            throw new ArgumentOutOfRangeException(nameof(balance));

        Balance = balance;
    }
}