namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ExcludeFromCodeCoverage]
    public class Pricing : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerApiRequest { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerSentEmail { get; set; }

        [Required]
        [MaxLength(3)]
        public string CurrencyIso { get; set; }

        [Required]
        public int Terms { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }

        public DateTime? DiscountMaturity { get; set; }

        public User User { get; set; }
    }
}