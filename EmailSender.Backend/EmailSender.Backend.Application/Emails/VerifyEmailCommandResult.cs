using EmailSender.Services.SmtpService.Models;

namespace EmailSender.Backend.Application.Emails;

public class VerifyEmailCommandResult
{
    public IEnumerable<VerifyEmail> CheckResult { get; set; }
}