namespace EmailSender.Backend.Cqrs.Responses
{
    using System;

    public class GetUserDetailsResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserAlias { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Registered { get; set; }

        public string Status { get; set; }
    }
}