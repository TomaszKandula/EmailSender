using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class UserAllowedIpsConfiguration : IEntityTypeConfiguration<UserAllowedIps>
{
    public void Configure(EntityTypeBuilder<UserAllowedIps> builder)
    {
        builder.Property(ips => ips.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(ips => ips.Users)
            .WithMany(users => users.UserAllowedIps)
            .HasForeignKey(ips => ips.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserAllowedIps_Users");
    }
}