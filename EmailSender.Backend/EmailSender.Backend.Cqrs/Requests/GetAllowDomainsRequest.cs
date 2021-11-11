namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowDomainsRequest : IRequest<GetAllowDomainsResponse>
    {
        public string PrivateKey { get; set; }
    }
}