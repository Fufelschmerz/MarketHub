namespace MarketHub.Persistence.Configurations.Users.Recoveries;

using MarketHub.Domain.Entities.Users.Recoveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class PasswordRecoveryConfiguration : IEntityTypeConfiguration<PasswordRecovery>
{
    public void Configure(EntityTypeBuilder<PasswordRecovery> builder)
    {
        builder.ToTable("PasswordRecoveries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<PasswordRecovery>(x=> x.UserId)
            .IsRequired();

        builder.Property(x => x.Token)
            .HasMaxLength(255)
            .IsRequired();
    }
}