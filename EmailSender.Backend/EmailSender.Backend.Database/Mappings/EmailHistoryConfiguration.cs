namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class EmailHistoryConfiguration : IEntityTypeConfiguration<EmailHistory>
    {
        public void Configure(EntityTypeBuilder<EmailHistory> builder)
        {
            builder.Property(emailHistory => emailHistory.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(emailHistory => emailHistory.Users)
                .WithMany(users => users.EmailHistory)
                .HasForeignKey(emailHistory => emailHistory.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailHistory_Users");

            builder
                .HasOne(emailHistory => emailHistory.Email)
                .WithMany(email => email.EmailHistory)
                .HasForeignKey(emailHistory => emailHistory.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailHistory_Email");
        }
    }
}