namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Billing : Entity<Guid>
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string CurrencyIso { get; set; }

        public DateTime ValueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? InvoiceSentDate { get; set; }

        public byte[] IssuedInvoice { get; set; }

        public User User { get; set; }
    }
}