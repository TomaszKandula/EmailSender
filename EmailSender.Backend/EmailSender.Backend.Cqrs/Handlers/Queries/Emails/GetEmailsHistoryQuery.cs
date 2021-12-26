namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using MediatR;

    public class GetEmailsHistoryQuery : IRequest<GetEmailsHistoryQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}