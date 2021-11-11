namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetAllowEmailsQueryRequest : IRequest<GetAllowEmailsQueryResponse>
    {
        public string PrivateKey { get; set; }
    }
}