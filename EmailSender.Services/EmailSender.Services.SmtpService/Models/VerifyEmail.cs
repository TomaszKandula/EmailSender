using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.SmtpService.Models;

[ExcludeFromCodeCoverage]
public class VerifyEmail
{
    public string Address { get; set; } = "";

    public bool IsFormatCorrect { get; set; }

    public bool IsDomainValid { get; set; }
}