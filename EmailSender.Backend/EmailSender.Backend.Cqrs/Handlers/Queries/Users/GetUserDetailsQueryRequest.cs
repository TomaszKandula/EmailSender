namespace EmailSender.Backend.Cqrs.Requests
{
    using MediatR;
    using Responses;

    public class GetUserDetailsQueryRequest : IRequest<GetUserDetailsQueryResponse>
    {
        public string PrivateKey { get; set; }
    }
}