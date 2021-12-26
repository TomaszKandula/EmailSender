namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;
    using SmtpService.Models;

    public class VerifyEmailCommandResponse
    {
        public IEnumerable<VerifyEmail> CheckResult { get; set; }
    }
}