using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class EmailsConfiguration : IEntityTypeConfiguration<Emails>
{
    public void Configure(EntityTypeBuilder<Emails> builder) 
        => builder.Property(emails => emails.Id).ValueGeneratedOnAdd();
}