namespace EmailSender.Backend.Cqrs.Requests
{
    using MediatR;
    using Responses;
    using System.Collections.Generic;

    public class VerifyEmailCommandRequest : IRequest<VerifyEmailCommandResponse>
    {
        public string PrivateKey { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}