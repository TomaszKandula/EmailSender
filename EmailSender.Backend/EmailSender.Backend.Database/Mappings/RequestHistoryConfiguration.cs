namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class RequestHistoryConfiguration : IEntityTypeConfiguration<RequestHistory>
    {
        public void Configure(EntityTypeBuilder<RequestHistory> builder)
        {
            builder.Property(requestHistory => requestHistory.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(requestHistory => requestHistory.Users)
                .WithMany(users => users.RequestHistory)
                .HasForeignKey(requestHistory => requestHistory.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestHistory_Users");
        }
    }
}