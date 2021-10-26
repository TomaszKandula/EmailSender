namespace EmailSender.Backend.SmtpService.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class VerifyEmail
    {
        public string Address { get; set; }

        public bool IsFormatCorrect { get; set; }

        public bool IsDomainValid { get; set; }
    }
}