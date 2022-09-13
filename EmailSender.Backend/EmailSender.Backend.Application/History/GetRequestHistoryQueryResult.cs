using EmailSender.Backend.Shared.Models;

namespace EmailSender.Backend.Application.History;

public class GetRequestHistoryQueryResult
{
    public string AssociatedUser { get; set; } = "";

    public List<RequestHistoryEntry> HistoryEntries { get; set; } = new();
}