namespace MarketHub.Domain.Entities.Users;

using Abstractions;
using Accounts;
using Infrastructure.Domain.Entities;
using Roles;
using ValueObjects.Security;

public sealed class User : Entity,
    IHasUniqueName
{
    private readonly List<Role> _roles = new();

    private User()
    {
    }

    public User(string name,
        string email,
        string password,
        IEnumerable<Role> roles)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        Name = name;
        Email = email;
        Password = new(password);

        AddRoles(roles);
    }

    public Account Account { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public Password Password { get; private set; }

    public IEnumerable<Role> Roles => _roles.AsEnumerable();

    protected internal void AddRoles(IEnumerable<Role> roles)
    {
        foreach (Role role in roles)
        {
            if (role is null)
                throw new ArgumentNullException(nameof(role));

            _roles.Add(role);
        }
    }
}