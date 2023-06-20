namespace MarketHub.Persistence.Configurations.Messaging.EmailMessaging;

using Domain.Entities.Messaging.EmailMessaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class EmailMessageTemplateConfiguration : IEntityTypeConfiguration<EmailMessageTemplate>
{
    public void Configure(EntityTypeBuilder<EmailMessageTemplate> builder)
    {
        builder.ToTable("EmailMessageTemplates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmailMessageTemplateType)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.UpdatedAtUtc)
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Body)
            .HasColumnType("Text")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}