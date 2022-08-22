namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using System.Collections.Generic;

public class GetUserAddressesQueryResult
{
    public IEnumerable<string> Addresses { get; set; }
}