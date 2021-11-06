namespace EmailSender.Backend.EmailService.Services.BillingService.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Billing
    {
        public decimal Amount { get; set; }

        public string CurrencyIso { get; set; }

        public DateTime ValueDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsInvoiceSent { get; set; }
    }
}