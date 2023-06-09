namespace MarketHub.Domain.Services.Tokens;

using Infrastructure.Domain.Services;

public interface ITokenService : IDomainService
{
    string Create();
}