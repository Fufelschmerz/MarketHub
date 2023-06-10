namespace MarketHub.Domain.Entities.Users.Roles;

using Enums;
using Infrastructure.Domain.Entities;
using Abstractions;
using Users;

public sealed class Role : Entity, IHasName
{
    private readonly List<User> _users = new();

    private Role()
    {
    }

    public Role(RoleType roleType,
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
        RoleType = roleType;
    }

    public RoleType RoleType { get; private set; }

    public string Name { get; private set; }

    public IEnumerable<User> Users => _users.AsEnumerable();
}