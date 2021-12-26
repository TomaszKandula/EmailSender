namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using System.Collections.Generic;
    using Shared.Models;

    public class GetEmailsHistoryQueryResult
    {
        public string AssociatedUser { get; set; }
        
        public IEnumerable<HistoryEntry> HistoryEntries { get; set; }
    }
}