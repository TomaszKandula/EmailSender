namespace EmailSenderService.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class RegisteredEmailConfiguration : IEntityTypeConfiguration<RegisteredEmail>
    {
        public void Configure(EntityTypeBuilder<RegisteredEmail> builder)
        {
            builder.Property(registeredEmail => registeredEmail.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(registeredEmail => registeredEmail.User)
                .WithMany(user => user.RegisteredEmail)
                .HasForeignKey(registeredEmail => registeredEmail.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredEmail_User");

            builder
                .HasOne(registeredEmail => registeredEmail.Email)
                .WithMany(email => email.RegisteredEmail)
                .HasForeignKey(history => history.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegisteredEmail_Email");
        }
    }
}