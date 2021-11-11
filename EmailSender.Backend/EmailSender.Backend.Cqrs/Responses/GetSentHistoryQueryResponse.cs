namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;
    using Shared.Models;

    public class GetSentHistoryQueryResponse
    {
        public string AssociatedUser { get; set; }
        
        public IEnumerable<HistoryEntry> HistoryEntries { get; set; }
    }
}