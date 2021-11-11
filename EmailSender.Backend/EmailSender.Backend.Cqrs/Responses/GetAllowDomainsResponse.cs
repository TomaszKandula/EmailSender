namespace EmailSender.Backend.Cqrs.Responses
{
    using System.Collections.Generic;

    public class GetAllowDomainsResponse
    {
        public IEnumerable<string> Hosts { get; set; }
    }
}