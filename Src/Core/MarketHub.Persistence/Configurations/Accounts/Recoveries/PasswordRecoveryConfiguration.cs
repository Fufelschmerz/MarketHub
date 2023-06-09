namespace MarketHub.Persistence.Configurations.Accounts.Recoveries;

using Domain.Entities.Accounts.Recoveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class PasswordRecoveryConfiguration : IEntityTypeConfiguration<PasswordRecovery>
{
    public void Configure(EntityTypeBuilder<PasswordRecovery> builder)
    {
        builder.ToTable("PasswordRecoveries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
        
        builder.HasOne(x => x.Account)
            .WithOne()
            .HasForeignKey<PasswordRecovery>(x=> x.AccountId)
            .IsRequired();

        builder.Property(x => x.Token)
            .HasMaxLength(255)
            .IsRequired();
    }
}