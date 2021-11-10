namespace EmailSender.Backend.AppService.Responses
{
    using BillingService.Models;

    public class GetUserBillingResponse : Billing
    {
        public string UserAlias { get; set; }
    }
}