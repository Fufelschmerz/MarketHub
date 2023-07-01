namespace MarketHub.Application.Services.Queries.Confirmations.EmailConfirmation.Handlers;

using Domain.Entities.Accounts.Confirmations;
using Domain.Specifications.Accounts.Confirmations;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindEmailConfirmationByAccountQueryHandler : IQueryHandler<FindEmailConfirmationByAccountQuery,
    EmailConfirmation?>
{
    private readonly IDbRepository<EmailConfirmation> _emailConfirmationDbRepository;

    public FindEmailConfirmationByAccountQueryHandler(IDbRepository<EmailConfirmation> emailConfirmationDbRepository)
    {
        _emailConfirmationDbRepository = emailConfirmationDbRepository ?? throw new ArgumentNullException(nameof(emailConfirmationDbRepository));
    }

    public Task<EmailConfirmation?> HandleAsync(FindEmailConfirmationByAccountQuery query,
        CancellationToken cancellationToken = default)
    {
        EmailConfirmationByAccountSpecification emailConfirmationByAccountSpec = new(query.Account);

        return _emailConfirmationDbRepository.SingleOrDefaultAsync(emailConfirmationByAccountSpec,
            cancellationToken);
    }
}