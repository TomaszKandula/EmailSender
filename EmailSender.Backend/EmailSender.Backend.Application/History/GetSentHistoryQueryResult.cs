using System.Collections.Generic;
using EmailSender.Backend.Shared.Models;

namespace EmailSender.Backend.Application.History;

public class GetSentHistoryQueryResult
{
    public string AssociatedUser { get; set; }
        
    public IEnumerable<SentHistoryEntry> HistoryEntries { get; set; }
}