namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class UpdateUserDetailsCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }

    public string CompanyName { get; set; }

    public string VatNumber { get; set; }

    public string StreetAddress { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string City { get; set; }
}
