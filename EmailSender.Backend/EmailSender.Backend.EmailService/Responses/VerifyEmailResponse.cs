namespace EmailSender.Backend.EmailService.Responses
{
    using System.Collections.Generic;
    using SmtpService.Models;

    public class VerifyEmailResponse
    {
        public IEnumerable<VerifyEmail> CheckResult { get; set; }
    }
}