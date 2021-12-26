namespace EmailSender.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class VatNumberPatternsConfiguration : IEntityTypeConfiguration<VatNumberPatterns>
    {
        public void Configure(EntityTypeBuilder<VatNumberPatterns> builder)
            => builder.Property(patterns => patterns.Id).ValueGeneratedOnAdd();
    }
}