namespace MarketHub.Domain.Services.Users;

using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Exceptions.Users;
using MarketHub.Domain.Repositories.Users;
using Common.UniqueName.Extensions;
using Specifications;
using MarketHub.Domain.Specifications.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task CreateAsync(User user,
        CancellationToken cancellationToken = default)
    {
        await this.CheckNameIsPossibleAsync(user,
            user.Name,
            cancellationToken);

        await CheckIsUserWithEmailExistAsync(user,
            user.Email,
            cancellationToken);

        await _userRepository.AddAsync(user,
            cancellationToken);
    }

    public Task<User?> FindWithSameNameAsync(string newName,
        CancellationToken cancellationToken = default)
    {
        EntityByNameSpecification<User> entityByNameSpec = new(newName);

        return _userRepository.SingleOrDefaultAsync(entityByNameSpec,
            cancellationToken);
    }

    private async Task CheckIsUserWithEmailExistAsync(User user,
        string newEmail,
        CancellationToken cancellationToken = default)
    {
        UserByEmailSpecification userByEmailSpec = new(newEmail);

        User? existingUser = await _userRepository.SingleOrDefaultAsync(userByEmailSpec,
            cancellationToken);

        if (existingUser != null && !Equals(user, existingUser))
            throw new UserWithSameEmailAlreadyExistsException($"User with email {newEmail} already exists");
    }
}