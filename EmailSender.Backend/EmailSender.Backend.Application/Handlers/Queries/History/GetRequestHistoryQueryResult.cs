using System.Collections.Generic;
using EmailSender.Backend.Shared.Models;

namespace EmailSender.Backend.Application.Handlers.Queries.History;

public class GetRequestHistoryQueryResult
{
    public string AssociatedUser { get; set; }

    public IEnumerable<RequestHistoryEntry> HistoryEntries { get; set; }
}