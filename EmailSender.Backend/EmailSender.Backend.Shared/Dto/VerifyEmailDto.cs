namespace EmailSender.Backend.Shared.Dto
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class VerifyEmailDto
    {
        public string PrivateKey { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}