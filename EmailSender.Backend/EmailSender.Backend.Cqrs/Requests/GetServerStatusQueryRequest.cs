namespace EmailSender.Backend.Cqrs.Requests
{
    using MediatR;

    public class GetServerStatusQueryRequest : IRequest<Unit>
    {
        public string PrivateKey { get; set; }

        public string EmailAddress { get; set; }
    }
}