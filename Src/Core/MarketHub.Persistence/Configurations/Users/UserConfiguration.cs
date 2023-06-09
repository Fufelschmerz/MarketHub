namespace MarketHub.Persistence.Configurations.Users;

using Domain.Entities.Accounts;
using Domain.Entities.Users;
using Domain.Entities.Users.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.OwnsOne(x => x.Password);

        builder.HasOne(x => x.Account)
            .WithOne(x => x.User)
            .HasForeignKey<Account>(x => x.UserId)
            .IsRequired();

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UsersToRoles",
                x => x.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                x => x.HasOne<User>().WithMany().HasForeignKey("UserId"));
    }
}