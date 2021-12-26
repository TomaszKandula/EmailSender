namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetAllUserBillingsQueryRequest : IRequest<GetAllUserBillingsQueryResponse>
    {
        public string PrivateKey { get; set; }
    }
}