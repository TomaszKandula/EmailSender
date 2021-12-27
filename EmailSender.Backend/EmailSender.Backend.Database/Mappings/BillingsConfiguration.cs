namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class BillingsConfiguration : IEntityTypeConfiguration<Billings>
{
    public void Configure(EntityTypeBuilder<Billings> builder)
    {
        builder.Property(billings => billings.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(billings => billings.Users)
            .WithMany(users => users.Billings)
            .HasForeignKey(billings => billings.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Billings_Users");
    }
}