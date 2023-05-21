namespace MarketHub.Domain.Entities.Accounts;

using Exceptions.Accounts;
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
        User = user;
        Balance = balance;
    }

    public long UserId { get; private set; }

    public User User { get; private set; }

    public decimal Balance { get; private set; }

    public bool IsConfirmed { get; private set; }

    public void Confirm()
    {
        if (IsConfirmed)
            throw new AccountAlreadyConfirmedException("Account is already confirmed");

        IsConfirmed = true;
    }
}