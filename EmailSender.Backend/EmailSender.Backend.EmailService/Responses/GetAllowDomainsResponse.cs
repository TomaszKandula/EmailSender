namespace EmailSender.Backend.EmailService.Responses
{
    using System.Collections.Generic;

    public class GetAllowDomainsResponse
    {
        public IEnumerable<string> Hosts { get; set; }
    }
}