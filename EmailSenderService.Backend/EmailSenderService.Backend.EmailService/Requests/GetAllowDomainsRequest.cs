namespace EmailSenderService.Backend.EmailService.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowDomainsRequest : IRequest<GetAllowDomainsResponse>
    {
        public string PrivateKey { get; set; }
    }
}