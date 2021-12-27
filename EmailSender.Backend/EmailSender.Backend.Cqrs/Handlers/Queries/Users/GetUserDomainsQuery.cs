namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using MediatR;

public class GetUserDomainsQuery : IRequest<GetUserDomainsQueryResult>
{
    public string PrivateKey { get; set; }
}