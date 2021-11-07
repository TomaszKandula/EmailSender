namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class VatNumberPatternConfiguration : IEntityTypeConfiguration<VatNumberPattern>
    {
        public void Configure(EntityTypeBuilder<VatNumberPattern> builder)
            => builder.Property(regex => regex.Id).ValueGeneratedOnAdd();
    }
}