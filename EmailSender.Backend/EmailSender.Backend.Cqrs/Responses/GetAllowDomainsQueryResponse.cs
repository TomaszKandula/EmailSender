namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;

    public class GetAllowDomainsQueryResponse
    {
        public IEnumerable<string> Hosts { get; set; }
    }
}