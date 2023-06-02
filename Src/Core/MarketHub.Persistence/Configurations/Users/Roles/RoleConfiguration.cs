namespace MarketHub.Persistence.Configurations.Users.Roles;

using MarketHub.Domain.Entities.Users.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
    }
}