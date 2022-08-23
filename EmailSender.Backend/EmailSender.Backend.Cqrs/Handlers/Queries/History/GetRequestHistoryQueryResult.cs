namespace EmailSender.Backend.Cqrs.Handlers.Queries.History;

using System.Collections.Generic;
using Shared.Models;

public class GetRequestHistoryQueryResult
{
    public string AssociatedUser { get; set; }

    public IEnumerable<RequestHistoryEntry> HistoryEntries { get; set; }
}