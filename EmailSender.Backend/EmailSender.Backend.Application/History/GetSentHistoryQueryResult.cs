using EmailSender.Backend.Shared.Models;

namespace EmailSender.Backend.Application.History;

public class GetSentHistoryQueryResult
{
    public string AssociatedUser { get; set; } = "";
        
    public List<SentHistoryEntry> HistoryEntries { get; set; } = new();
}