namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserDomainsConfiguration : IEntityTypeConfiguration<UserDomains>
{
    public void Configure(EntityTypeBuilder<UserDomains> builder)
    {
        builder.Property(userDomains => userDomains.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(userDomains => userDomains.Users)
            .WithMany(users => users.UserDomains)
            .HasForeignKey(userDomains => userDomains.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserDomains_Users");
    }
}