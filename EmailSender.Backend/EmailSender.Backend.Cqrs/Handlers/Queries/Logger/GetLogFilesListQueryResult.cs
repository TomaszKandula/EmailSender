using System.Collections.Generic;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Logger;

public class GetLogFilesListQueryResult
{
    public List<string> LogFiles { get; set; }
}