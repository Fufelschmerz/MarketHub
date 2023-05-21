namespace MarketHub.Domain.Services.Users;

using MarketHub.Domain.Entities.Users;
using MarketHub.Domain.Entities.Users.Roles;
using MarketHub.Domain.Exceptions.Users;
using MarketHub.Domain.Repositories.Users;
using MarketHub.Domain.Services.Common.UniqueName.Extensions;
using MarketHub.Domain.Specifications;
using MarketHub.Domain.Specifications.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User> CreateAsync(string name,
        string email,
        string password,
        IEnumerable<Role> roles,
        CancellationToken cancellationToken = default)
    {
        User user = new(name,
            email,
            password,
            roles);

        await this.CheckNameIsPossibleAsync(user,
            user.Name,
            cancellationToken);

        await CheckIsUserWithEmailExistAsync(user,
            user.Email,
            cancellationToken);

        await _userRepository.AddAsync(user,
            cancellationToken);

        return user;
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