namespace MarketHub.Application.Services.Queries.PasswordRecoveries.Handlers;

using Domain.Entities.Users.Recoveries;
using Domain.Specifications.Users.Recoveries;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindPasswordRecoveryByUserQueryHandler : IQueryHandler<FindPasswordRecoveryByUserQuery, 
    PasswordRecovery?>
{
    private readonly IDbRepository<PasswordRecovery> _passwordRecoveryDbRepository;

    public FindPasswordRecoveryByUserQueryHandler(IDbRepository<PasswordRecovery> passwordRecoveryDbRepository)
    {
        _passwordRecoveryDbRepository = passwordRecoveryDbRepository ?? throw new ArgumentNullException(nameof(passwordRecoveryDbRepository));
    }

    public Task<PasswordRecovery?> HandleAsync(FindPasswordRecoveryByUserQuery query,
        CancellationToken cancellationToken = default)
    {
        PasswordRecoveryByUserSpecification passwordRecoveryByUserSpec = new(query.User);
        
        return _passwordRecoveryDbRepository.SingleOrDefaultAsync(passwordRecoveryByUserSpec, 
            cancellationToken);
    }
}