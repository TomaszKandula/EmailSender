using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class RequestsHistory : Entity<Guid>
{
    public Guid UserId { get; set; }

    [Required]
    public DateTime RequestedAt { get; set; }
        
    [Required]
    [MaxLength(255)]
    public string RequestName { get; set; }

    public Users Users { get; set; }
}