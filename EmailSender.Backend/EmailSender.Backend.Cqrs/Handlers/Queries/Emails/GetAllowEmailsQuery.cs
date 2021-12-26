namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using MediatR;

    public class GetAllowEmailsQuery : IRequest<GetAllowEmailsQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}