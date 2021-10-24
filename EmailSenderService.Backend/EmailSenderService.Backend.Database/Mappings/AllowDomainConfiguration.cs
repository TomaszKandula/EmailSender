namespace EmailSenderService.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class AllowDomainConfiguration : IEntityTypeConfiguration<AllowDomain>
    {
        public void Configure(EntityTypeBuilder<AllowDomain> builder)
        {
            builder.Property(allowDomain => allowDomain.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(allowDomain => allowDomain.User)
                .WithMany(user => user.AllowDomain)
                .HasForeignKey(allowDomain => allowDomain.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllowDomain_User");
        }
    }
}