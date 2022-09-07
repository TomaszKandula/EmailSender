using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class UsersConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder) 
        => builder.Property(users => users.Id).ValueGeneratedOnAdd();
}