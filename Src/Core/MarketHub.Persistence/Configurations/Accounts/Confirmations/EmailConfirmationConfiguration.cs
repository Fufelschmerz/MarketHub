namespace MarketHub.Persistence.Configurations.Accounts.Confirmations;

using Domain.Entities.Accounts.Confirmations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class EmailConfirmationConfiguration : IEntityTypeConfiguration<EmailConfirmation>
{
    public void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        builder.ToTable("EmailConfirmations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.CompletedAtUtc)
            .HasDefaultValue(null);

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Token)
            .HasMaxLength(255)
            .IsRequired();
    }
}