using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserAllowedIps : Entity<Guid>
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }

    public Users Users { get; set; }
}