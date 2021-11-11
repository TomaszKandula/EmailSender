namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowDomainsQueryRequest : IRequest<GetAllowDomainsQueryResponse>
    {
        public string PrivateKey { get; set; }
    }
}