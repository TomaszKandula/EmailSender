namespace EmailSender.Backend.EmailService.Responses
{
    using Services.BillingService.Models;

    public class GetUserBillingResponse : Billing
    {
        public string UserAlias { get; set; }
    }
}