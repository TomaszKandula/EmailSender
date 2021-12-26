namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class RequestsHistoryConfiguration : IEntityTypeConfiguration<RequestsHistory>
    {
        public void Configure(EntityTypeBuilder<RequestsHistory> builder)
        {
            builder.Property(requestsHistory => requestsHistory.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(requestsHistory => requestsHistory.Users)
                .WithMany(users => users.RequestsHistory)
                .HasForeignKey(requestsHistory => requestsHistory.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestsHistory_Users");
        }
    }
}