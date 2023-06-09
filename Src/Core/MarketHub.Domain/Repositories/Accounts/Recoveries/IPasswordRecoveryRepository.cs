namespace MarketHub.Domain.Repositories.Accounts.Recoveries;

using Entities.Accounts.Recoveries;
using Infrastructure.Persistence.Repositories;

public interface IPasswordRecoveryRepository : IRepository<PasswordRecovery>
{
}