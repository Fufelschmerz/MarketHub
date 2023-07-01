namespace MarketHub.Application.Services.Queries.Users.Handlers;

using Domain.Entities.Users;
using Domain.Specifications.Users;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindUserByEmailQueryHandler : IQueryHandler<FindUserByEmailQuery, User?>
{
    private readonly IDbRepository<User> _userDbRepository;

    public FindUserByEmailQueryHandler(IDbRepository<User> userDbRepository)
    {
        _userDbRepository = userDbRepository ?? throw new ArgumentNullException(nameof(userDbRepository));
    }
    
    public Task<User?> HandleAsync(FindUserByEmailQuery query,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(query.Email);

        return _userDbRepository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);
    }
}