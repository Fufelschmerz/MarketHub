namespace MarketHub.Application.Services.Queries.PasswordRecoveries.Handlers;

using Domain.Entities.Users.Recoveries;
using Domain.Specifications.Users.Recoveries;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindPasswordRecoveryByUserQueryHandler : IQueryHandler<FindPasswordRecoveryByUserQuery, 
    PasswordRecovery?>
{
    private readonly IRepository<PasswordRecovery> _passwordRecoveryRepository;

    public FindPasswordRecoveryByUserQueryHandler(IRepository<PasswordRecovery> passwordRecoveryRepository)
    {
        _passwordRecoveryRepository = passwordRecoveryRepository ?? throw new ArgumentNullException(nameof(passwordRecoveryRepository));
    }

    public Task<PasswordRecovery?> HandleAsync(FindPasswordRecoveryByUserQuery query,
        CancellationToken cancellationToken = default)
    {
        PasswordRecoveryByUserSpecification passwordRecoveryByUserSpec = new(query.User);
        
        return _passwordRecoveryRepository.SingleOrDefaultAsync(passwordRecoveryByUserSpec, 
            cancellationToken);
    }
}