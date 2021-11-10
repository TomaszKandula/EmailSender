namespace EmailSender.Backend.AppService.Responses
{
    using System.Collections.Generic;

    public class GetAllowEmailsResponse
    {
        public string AssociatedUser { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}