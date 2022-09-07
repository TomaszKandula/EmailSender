namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using System.Collections.Generic;

public class GetUserAllowedIpsQueryResult
{
    public IEnumerable<string> IpList { get; set; }
}