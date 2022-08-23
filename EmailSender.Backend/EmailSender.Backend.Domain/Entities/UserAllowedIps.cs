namespace EmailSender.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class UserAllowedIps : Entity<Guid>
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }

    public Users Users { get; set; }
}