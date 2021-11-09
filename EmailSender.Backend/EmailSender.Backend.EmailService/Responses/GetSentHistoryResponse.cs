namespace EmailSender.Backend.EmailService.Responses
{
    using System.Collections.Generic;
    using Shared.Models;

    public class GetSentHistoryResponse
    {
        public string AssociatedUser { get; set; }
        
        public IEnumerable<HistoryEntry> HistoryEntries { get; set; }
    }
}