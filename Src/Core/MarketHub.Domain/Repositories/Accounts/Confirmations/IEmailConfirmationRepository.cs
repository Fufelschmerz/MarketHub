namespace MarketHub.Domain.Repositories.Accounts.Confirmations;

using Entities.Accounts.Confirmations;
using Infrastructure.Persistence.Repositories;

public interface IEmailConfirmationRepository : IRepository<EmailConfirmation>
{
}