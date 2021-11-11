namespace EmailSender.Backend.Cqrs.Requests
{
    using Responses;
    using MediatR;

    public class GetAllUserBillingsRequest : IRequest<GetAllUserBillingsResponse>
    {
        public string PrivateKey { get; set; }
    }
}