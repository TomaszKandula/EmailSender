namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class AllowEmailConfiguration : IEntityTypeConfiguration<AllowEmail>
    {
        public void Configure(EntityTypeBuilder<AllowEmail> builder)
        {
            builder.Property(allowEmail => allowEmail.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(allowEmail => allowEmail.Users)
                .WithMany(users => users.AllowEmail)
                .HasForeignKey(allowEmail => allowEmail.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllowEmail_Users");

            builder
                .HasOne(allowEmail => allowEmail.Emails)
                .WithMany(emails => emails.AllowEmail)
                .HasForeignKey(allowEmail => allowEmail.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllowEmail_Emails");
        }
    }
}