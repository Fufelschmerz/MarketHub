namespace MarketHub.Domain.Services.Users;

using Common.UniqueName;
using Common.UniqueName.Extensions;
using Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Exceptions.Users;
using Specifications;
using Specifications.Users;

public sealed class UserService : UniqueNameService<User>, IUserService
{
    private readonly IDbRepository<User> _userDbRepository;

    public UserService(IDbRepository<User> userDbRepository)
    {
        _userDbRepository = userDbRepository ?? throw new ArgumentNullException(nameof(userDbRepository));
    }

    public async Task CreateAsync(User user,
        CancellationToken cancellationToken = default)
    {
        await this.CheckNameIsPossibleAsync(user.Name,
            cancellationToken);

        await CheckIsUserWithEmailExistAsync(user.Email,
            cancellationToken);

        await _userDbRepository.AddAsync(user,
            cancellationToken);
    }

    private async Task CheckIsUserWithEmailExistAsync(string email,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(email);

        User? existingUser = await _userDbRepository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);

        if (existingUser is not null)
            throw new UserWithSameEmailAlreadyExistsException($"User with email {email} already exists");
    }

    protected internal override Task<User?> FindWithSameNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        EntityByNameSpecification<User> entityByNameSpec = new(name);

        return _userDbRepository.SingleOrDefaultAsync(entityByNameSpec,
            cancellationToken);
    }
}