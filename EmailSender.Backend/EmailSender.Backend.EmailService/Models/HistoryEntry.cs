namespace EmailSender.Backend.EmailService.Models
{
    using System;

    public class HistoryEntry
    {
        public string EmailFrom { get; set; }
        
        public DateTime SentAt { get; set; }
    }
}