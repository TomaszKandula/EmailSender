namespace EmailSender.Backend.AppService.Requests
{
    using MediatR;

    public class GetServerStatusRequest : IRequest<Unit>
    {
        public string PrivateKey { get; set; }

        public string EmailAddress { get; set; }
    }
}