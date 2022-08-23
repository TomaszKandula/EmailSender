namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

using System.Collections.Generic;
using Shared.Models;

public class GetSentHistoryQueryResult
{
    public string AssociatedUser { get; set; }
        
    public IEnumerable<HistoryEntry> HistoryEntries { get; set; }
}