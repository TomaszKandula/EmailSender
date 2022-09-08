using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserDetails : Entity<Guid>
{
    public Guid UserId { get; set; }

    [MaxLength(255)]
    public string CompanyName { get; set; }

    [MaxLength(25)]
    public string VatNumber { get; set; }

    [Required]
    [MaxLength(255)]
    public string StreetAddress { get; set; }

    [Required]
    [MaxLength(12)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string Country { get; set; }

    [Required]
    [MaxLength(255)]
    public string City { get; set; }

    public Users Users { get; set; }
}