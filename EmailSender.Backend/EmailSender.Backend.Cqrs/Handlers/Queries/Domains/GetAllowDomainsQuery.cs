namespace EmailSender.Backend.Cqrs.Handlers.Queries.Domains
{
    using MediatR;

    public class GetAllowDomainsQuery : IRequest<GetAllowDomainsQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}