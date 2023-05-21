namespace MarketHub.Domain.Services.Accounts.Confirmations;

using Infrastructure.Domain.Services;

public interface IConfirmationTokenGenerator : IDomainService
{
    string Create();
}