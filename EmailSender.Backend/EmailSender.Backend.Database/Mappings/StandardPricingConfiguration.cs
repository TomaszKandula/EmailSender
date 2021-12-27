namespace EmailSender.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class StandardPricingConfiguration : IEntityTypeConfiguration<StandardPricing>
{
    public void Configure(EntityTypeBuilder<StandardPricing> builder)
        => builder.Property(standardPricing => standardPricing.Id).ValueGeneratedOnAdd();
}