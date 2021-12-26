namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetSentHistoryQueryRequest : IRequest<GetSentHistoryQueryResponse>
    {
        public string PrivateKey { get; set; }
    }
}