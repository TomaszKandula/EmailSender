namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    [ExcludeFromCodeCoverage]
    public class StandardPricing : Entity<Guid>
    {
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
        public PricingTerms Terms { get; set; }
        
        [Required]
        public PricingStatuses Status { get; set; }
    }
}