using System.Collections.Generic;

namespace EmailSender.Backend.Application.Users.Queries;

public class GetUserEmailsQueryResult
{
    public string AssociatedUser { get; set; }

    public IEnumerable<string> Emails { get; set; }
}