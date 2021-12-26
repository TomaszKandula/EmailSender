namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users
{
    using System.Collections.Generic;

    public class GetUserEmailsQueryResult
    {
        public string AssociatedUser { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}