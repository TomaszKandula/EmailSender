using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Users : Entity<Guid>
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(5)]
    public string UserAlias { get; set; }

    [Required]
    [MaxLength(255)]
    public string EmailAddress { get; set; }

    public UserStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime Registered { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string PrivateKey { get; set; }

    public UserRole Role { get; set; }

    public ICollection<UserEmails> UserEmails { get; set; } = new HashSet<UserEmails>();

    public ICollection<SentHistory> SentHistory { get; set; } = new HashSet<SentHistory>();

    public ICollection<UserAllowedIps> UserAllowedIps { get; set; } = new HashSet<UserAllowedIps>();

    public ICollection<RequestsHistory> RequestsHistory { get; set; } = new HashSet<RequestsHistory>();

    public ICollection<UserDetails> UserDetails { get; set; } = new HashSet<UserDetails>();
}