namespace MarketHub.Application.Services.Queries.Users.Handlers;

using Domain.Entities.Users;
using Domain.Specifications.Users;
using global::Infrastructure.Application.Services.Queries;
using global::Infrastructure.Persistence.Repositories;

public sealed class FindUserByEmailQueryHandler : IQueryHandler<FindUserByEmailQuery, User?>
{
    private readonly IRepository<User> _userRepository;

    public FindUserByEmailQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public Task<User?> HandleAsync(FindUserByEmailQuery query,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(query.Email);

        return _userRepository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);
    }
}