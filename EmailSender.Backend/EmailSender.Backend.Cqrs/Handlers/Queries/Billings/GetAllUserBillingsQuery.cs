namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings
{
    using MediatR;

    public class GetAllUserBillingsQuery : IRequest<GetAllUserBillingsQueryResult>
    {
        public string PrivateKey { get; set; }
    }
}