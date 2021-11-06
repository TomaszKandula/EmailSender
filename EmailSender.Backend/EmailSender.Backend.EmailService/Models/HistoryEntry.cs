namespace EmailSender.Backend.EmailService.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class HistoryEntry
    {
        public string EmailFrom { get; set; }
        
        public DateTime SentAt { get; set; }
    }
}