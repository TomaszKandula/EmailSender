namespace EmailSender.Backend.EmailService.Responses
{
    using BillingService.Models;

    public class GetUserBillingResponse : Billing
    {
        public string UserAlias { get; set; }
    }
}