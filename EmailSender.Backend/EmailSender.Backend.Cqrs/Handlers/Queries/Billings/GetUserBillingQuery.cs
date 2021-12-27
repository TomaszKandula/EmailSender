namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings;

using System;
using MediatR;

public class GetUserBillingQuery : IRequest<GetUserBillingQueryResult>
{
    public string PrivateKey { get; set; }

    public Guid BillingId { get; set; }
}