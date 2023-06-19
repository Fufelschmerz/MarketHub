namespace MarketHub.Persistence.Extensions.Init.Roles;

using Domain.Entities.Users.Roles;
using Domain.Entities.Users.Roles.Enums;
using Microsoft.EntityFrameworkCore;

public static class InitRolesExtensions
{
    public static void InitRoles(this ModelBuilder modelBuilder)
    {
        IReadOnlyList<Role> roles = CreateRoles();

        foreach (Role role in roles)
        {
            modelBuilder.Entity<Role>().HasData(role);
        }
    }

    private static IReadOnlyList<Role> CreateRoles()
    {
        return new List<Role>()
        {
            new(RoleType.Admin, "Администратор")
            {
                Id = 1,
            },
            new(RoleType.User, "Пользователь")
            {
                Id = 2
            }
        };
    }
}