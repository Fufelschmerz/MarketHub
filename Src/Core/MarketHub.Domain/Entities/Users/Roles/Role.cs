namespace MarketHub.Domain.Entities.Users.Roles;

using Enums;
using Infrastructure.Domain.Entities;
using MarketHub.Domain.Abstractions;
using MarketHub.Domain.Entities.Users;

public sealed class Role : Entity,
    IHasName
{
    private readonly List<User> _users = new();

    public Role()
    {
    }

    public Role(RoleType type,
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
        Type = type;
    }

    public RoleType Type { get; private set; }

    public string Name { get; private set; }

    public IEnumerable<User> Users => _users.AsEnumerable();
}