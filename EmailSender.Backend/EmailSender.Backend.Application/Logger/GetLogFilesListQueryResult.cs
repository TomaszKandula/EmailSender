using System.Collections.Generic;

namespace EmailSender.Backend.Application.Logger;

public class GetLogFilesListQueryResult
{
    public List<string> LogFiles { get; set; }
}