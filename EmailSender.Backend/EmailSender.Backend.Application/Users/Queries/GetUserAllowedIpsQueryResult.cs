using System.Collections.Generic;

namespace EmailSender.Backend.Application.Handlers.Queries.Users;

public class GetUserAllowedIpsQueryResult
{
    public IEnumerable<string> IpList { get; set; }
}