namespace EmailSender.Backend.EmailService.Responses
{
    using System.Collections.Generic;
    using Services.BillingService.Models;

    public class GetAllUserBillingsResponse
    {
        public string UserAlias { get; set; }

        public IEnumerable<Billing> Billings { get; set; }
    }
}