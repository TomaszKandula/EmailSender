namespace EmailSender.Backend.Cqrs.Handlers.Commands.Emails;

using System.Collections.Generic;
using Services.SmtpService.Models;

public class VerifyEmailCommandResult
{
    public IEnumerable<VerifyEmail> CheckResult { get; set; }
}