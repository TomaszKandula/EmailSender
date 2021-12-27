namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.Property(userDetails => userDetails.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(userDetails => userDetails.Users)
            .WithMany(users => users.UserDetails)
            .HasForeignKey(userDetails => userDetails.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserDetails_Users");
    }
}