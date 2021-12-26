namespace EmailSender.Backend.Cqrs.Handlers.Queries.Domains
{
    using System.Collections.Generic;

    public class GetAllowDomainsQueryResult
    {
        public IEnumerable<string> Hosts { get; set; }
    }
}