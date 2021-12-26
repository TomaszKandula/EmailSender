namespace EmailSender.Backend.Cqrs.Responses
{
    using BillingService.Models;

    public class GetUserBillingQueryResponse : Billing
    {
        public string UserAlias { get; set; }
    }
}