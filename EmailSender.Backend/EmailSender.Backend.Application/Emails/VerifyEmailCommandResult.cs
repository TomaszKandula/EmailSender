using EmailSender.Services.SmtpService.Models;

namespace EmailSender.Backend.Application.Emails;

public class VerifyEmailCommandResult
{
    public List<VerifyEmail> CheckResult { get; set; } = new();
}