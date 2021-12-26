namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings
{
    using BillingService.Models;

    public class GetUserBillingQueryResult : Billing
    {
        public string UserAlias { get; set; }
    }
}