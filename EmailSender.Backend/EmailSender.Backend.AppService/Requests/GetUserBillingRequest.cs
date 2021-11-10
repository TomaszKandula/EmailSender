namespace EmailSender.Backend.AppService.Requests
{
    using System;
    using Responses;
    using MediatR;

    public class GetUserBillingRequest : IRequest<GetUserBillingResponse>
    {
        public string PrivateKey { get; set; }

        public Guid BillingId { get; set; }
    }
}