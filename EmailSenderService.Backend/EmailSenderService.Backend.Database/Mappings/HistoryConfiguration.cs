namespace EmailSenderService.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.Property(history => history.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(history => history.User)
                .WithMany(user => user.History)
                .HasForeignKey(history => history.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_User");

            builder
                .HasOne(history => history.Email)
                .WithMany(email => email.History)
                .HasForeignKey(history => history.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Email");
        }
    }
}