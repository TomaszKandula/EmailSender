namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class EmailsConfiguration : IEntityTypeConfiguration<Emails>
    {
        public void Configure(EntityTypeBuilder<Emails> builder) 
            => builder.Property(emails => emails.Id).ValueGeneratedOnAdd();
    }
}