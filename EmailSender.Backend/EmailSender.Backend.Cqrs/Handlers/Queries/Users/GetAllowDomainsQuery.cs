namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users
{
    using MediatR;

    public class GetAllowDomainsQuery : IRequest<GetAllowDomainsQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}