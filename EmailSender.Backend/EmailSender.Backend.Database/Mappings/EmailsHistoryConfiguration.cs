namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class EmailsHistoryConfiguration : IEntityTypeConfiguration<EmailsHistory>
{
    public void Configure(EntityTypeBuilder<EmailsHistory> builder)
    {
        builder.Property(emailsHistory => emailsHistory.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(emailsHistory => emailsHistory.Users)
            .WithMany(users => users.EmailsHistory)
            .HasForeignKey(emailsHistory => emailsHistory.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EmailsHistory_Users");

        builder
            .HasOne(emailsHistory => emailsHistory.Emails)
            .WithMany(emails => emails.EmailsHistory)
            .HasForeignKey(emailsHistory => emailsHistory.EmailId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EmailsHistory_Emails");
    }
}