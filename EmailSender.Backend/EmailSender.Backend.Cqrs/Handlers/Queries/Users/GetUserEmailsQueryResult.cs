using System.Collections.Generic;

namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

public class GetUserEmailsQueryResult
{
    public string AssociatedUser { get; set; }

    public IEnumerable<string> Emails { get; set; }
}