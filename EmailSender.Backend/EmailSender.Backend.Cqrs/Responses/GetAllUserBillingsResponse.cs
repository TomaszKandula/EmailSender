namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;
    using BillingService.Models;

    public class GetAllUserBillingsResponse
    {
        public string UserAlias { get; set; }

        public IEnumerable<Billing> Billings { get; set; }
    }
}