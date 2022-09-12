using System.Collections.Generic;
using EmailSender.Services.SmtpService.Models;

namespace EmailSender.Backend.Application.Handlers.Commands.Emails;

public class VerifyEmailCommandResult
{
    public IEnumerable<VerifyEmail> CheckResult { get; set; }
}