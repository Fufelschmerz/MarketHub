namespace MarketHub.Domain.Services.Users;

using Common.UniqueName;
using Common.UniqueName.Extensions;
using Infrastructure.Persistence.Repositories;
using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Exceptions.Users;
using Specifications;
using MarketHub.Domain.Specifications.Users;

public sealed class UserService : UniqueNameService<User>, IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task CreateAsync(User user,
        CancellationToken cancellationToken = default)
    {
        await this.CheckNameIsPossibleAsync(user.Name,
            cancellationToken);

        await CheckIsUserWithEmailExistAsync(user.Email,
            cancellationToken);

        await _userRepository.AddAsync(user,
            cancellationToken);
    }

    private async Task CheckIsUserWithEmailExistAsync(string newEmail,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(newEmail);

        User? existingUser = await _userRepository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);

        if (existingUser is not null)
            throw new UserWithSameEmailAlreadyExistsException($"User with email {newEmail} already exists");
    }

    protected internal override Task<User?> FindWithSameNameAsync(string newName,
        CancellationToken cancellationToken = default)
    {
        EntityByNameSpecification<User> entityByNameSpec = new(newName);

        return _userRepository.SingleOrDefaultAsync(entityByNameSpec,
            cancellationToken);
    }
}