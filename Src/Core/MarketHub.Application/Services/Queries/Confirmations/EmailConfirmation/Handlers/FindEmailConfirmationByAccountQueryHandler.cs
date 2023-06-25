namespace MarketHub.Application.Services.Queries.Confirmations.EmailConfirmation.Handlers;

using Domain.Entities.Accounts.Confirmations;
using Domain.Specifications.Accounts.Confirmations;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindEmailConfirmationByAccountQueryHandler : IQueryHandler<FindEmailConfirmationByAccountQuery,
    EmailConfirmation?>
{
    private readonly IRepository<EmailConfirmation> _emailConfirmationRepository;

    public FindEmailConfirmationByAccountQueryHandler(IRepository<EmailConfirmation> emailConfirmationRepository)
    {
        _emailConfirmationRepository = emailConfirmationRepository ?? throw new ArgumentNullException(nameof(emailConfirmationRepository));
    }

    public Task<EmailConfirmation?> HandleAsync(FindEmailConfirmationByAccountQuery query,
        CancellationToken cancellationToken = default)
    {
        EmailConfirmationByAccountSpecification emailConfirmationByAccountSpec = new(query.Account);

        return _emailConfirmationRepository.SingleOrDefaultAsync(emailConfirmationByAccountSpec,
            cancellationToken);
    }
}