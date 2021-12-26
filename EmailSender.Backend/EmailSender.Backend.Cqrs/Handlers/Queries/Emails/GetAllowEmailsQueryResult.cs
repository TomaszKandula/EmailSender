namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using System.Collections.Generic;

    public class GetAllowEmailsQueryResult
    {
        public string AssociatedUser { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}