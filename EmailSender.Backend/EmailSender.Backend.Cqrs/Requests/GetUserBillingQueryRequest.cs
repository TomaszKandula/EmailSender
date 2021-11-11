namespace EmailSender.Backend.Cqrs.Requests
{
    using System;
    using Responses;
    using MediatR;

    public class GetUserBillingQueryRequest : IRequest<GetUserBillingQueryResponse>
    {
        public string PrivateKey { get; set; }

        public Guid BillingId { get; set; }
    }
}