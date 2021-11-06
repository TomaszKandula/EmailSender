namespace EmailSender.Backend.EmailService.Services.BillingService.Models
{
    using System;

    public class Billing
    {
        public decimal Amount { get; set; }

        public string CurrencyIso { get; set; }

        public DateTime ValueDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsInvoiceSent { get; set; }

        public byte[] IssuedInvoice { get; set; }
    }
}