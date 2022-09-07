using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class SentHistoryConfiguration : IEntityTypeConfiguration<SentHistory>
{
    public void Configure(EntityTypeBuilder<SentHistory> builder)
    {
        builder.Property(history => history.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(history => history.Users)
            .WithMany(users => users.SentHistory)
            .HasForeignKey(history => history.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SentHistory_Users");

        builder
            .HasOne(history => history.Emails)
            .WithMany(emails => emails.SentHistory)
            .HasForeignKey(history => history.EmailId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SentHistory_Emails");
    }
}