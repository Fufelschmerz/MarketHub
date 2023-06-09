namespace MarketHub.Persistence.Configurations.Accounts;

using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsEmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false);
    }
}