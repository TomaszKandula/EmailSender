using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserEmailsConfiguration : IEntityTypeConfiguration<UserEmails>
{
    public void Configure(EntityTypeBuilder<UserEmails> builder)
    {
        builder.Property(userEmails => userEmails.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(userEmails => userEmails.Users)
            .WithMany(users => users.UserEmails)
            .HasForeignKey(userEmails => userEmails.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserEmails_Users");

        builder
            .HasOne(userEmails => userEmails.Emails)
            .WithMany(emails => emails.UserEmails)
            .HasForeignKey(userEmails => userEmails.EmailId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserEmails_Emails");
    }
}