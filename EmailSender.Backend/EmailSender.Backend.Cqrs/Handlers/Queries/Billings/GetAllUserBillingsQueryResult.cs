namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings
{
    using System.Collections.Generic;
    using BillingService.Models;

    public class GetAllUserBillingsQueryResult
    {
        public string UserAlias { get; set; }

        public IEnumerable<Billing> Billings { get; set; }
    }
}