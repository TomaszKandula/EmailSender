namespace EmailSenderService.Backend.SmtpService.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Email
    {
        public string Address { get; set; }

        public bool IsValid { get; set; }
    }
}