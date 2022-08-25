namespace EmailSender.Services.UserService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UpdateUserDetailsInput
{
    public Guid UserId { get; set; }

    public string CompanyName { get; set; }

    public string VatNumber { get; set; }

    public string StreetAddress { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string City { get; set; }
}