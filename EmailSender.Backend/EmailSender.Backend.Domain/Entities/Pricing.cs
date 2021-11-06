namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Pricing : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        public decimal PerApiRequest { get; set; }

        [Required]
        public decimal PerSentEmail { get; set; }

        [Required]
        [MaxLength(3)]
        public string CurrencyIso { get; set; }

        [Required]
        public int Terms { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? DiscountMaturity { get; set; }

        public User User { get; set; }
    }
}