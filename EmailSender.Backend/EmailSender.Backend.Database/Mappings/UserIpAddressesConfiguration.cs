namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserIpAddressesConfiguration : IEntityTypeConfiguration<UserIpAddresses>
{
    public void Configure(EntityTypeBuilder<UserIpAddresses> builder)
    {
        builder.Property(addresses => addresses.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(addresses => addresses.Users)
            .WithMany(users => users.UserIpAddresses)
            .HasForeignKey(addresses => addresses.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserIpAddresses_Users");
    }
}