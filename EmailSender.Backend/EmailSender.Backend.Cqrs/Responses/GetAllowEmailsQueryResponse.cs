namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;

    public class GetAllowEmailsQueryResponse
    {
        public string AssociatedUser { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}