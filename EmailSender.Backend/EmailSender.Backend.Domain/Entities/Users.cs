namespace EmailSender.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

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

    public bool IsActivated { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime Registered { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string PrivateKey { get; set; }

    public ICollection<UserEmails> UserEmails { get; set; } = new HashSet<UserEmails>();

    public ICollection<EmailsHistory> EmailsHistory { get; set; } = new HashSet<EmailsHistory>();

    public ICollection<UserDomains> UserDomains { get; set; } = new HashSet<UserDomains>();

    public ICollection<RequestsHistory> RequestsHistory { get; set; } = new HashSet<RequestsHistory>();

    public ICollection<UserDetails> UserDetails { get; set; } = new HashSet<UserDetails>();
}