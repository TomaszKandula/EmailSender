namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Price : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        public decimal PerApiRequest { get; set; }

        [Required]
        public decimal PerSentEmail { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? DiscountMaturity { get; set; }

        public User User { get; set; }
    }
}