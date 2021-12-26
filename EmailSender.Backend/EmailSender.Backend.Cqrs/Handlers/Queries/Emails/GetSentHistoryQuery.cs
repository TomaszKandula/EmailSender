namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using MediatR;

    public class GetSentHistoryQuery : IRequest<GetSentHistoryQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}