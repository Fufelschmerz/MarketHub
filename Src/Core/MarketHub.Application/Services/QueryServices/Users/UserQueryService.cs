namespace MarketHub.Application.Services.QueryServices.Users;

using Domain.Entities.Users;
using Domain.Repositories.Users;
using Domain.Specifications.Users;

public sealed class UserQueryService : QueryService<User, IUserRepository>,
    IUserQueryService
{
    public UserQueryService(IUserRepository repository)
        : base(repository)
    {
    }

    public Task<User?> FindByEmailAsync(string email,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(email);

        return _repository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);
    }
}