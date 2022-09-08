using System.Collections.Generic;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

public class GetUserAllowedIpsQueryResult
{
    public IEnumerable<string> IpList { get; set; }
}