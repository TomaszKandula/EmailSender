namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using System.Collections.Generic;

public class GetUserDomainsQueryResult
{
    public IEnumerable<string> Hosts { get; set; }
}