namespace EmailSenderService.Backend.Database.Mappings
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
                .HasOne(allowEmail => allowEmail.User)
                .WithMany(user => user.AllowEmail)
                .HasForeignKey(allowEmail => allowEmail.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllowEmail_User");

            builder
                .HasOne(allowEmail => allowEmail.Email)
                .WithMany(email => email.AllowEmail)
                .HasForeignKey(allowEmail => allowEmail.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllowEmail_Email");
        }
    }
}