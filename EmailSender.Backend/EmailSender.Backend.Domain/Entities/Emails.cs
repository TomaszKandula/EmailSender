namespace EmailSender.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class Emails : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Address { get; set; }

    public bool IsActive { get; set; }

    [Required]
    [MaxLength(255)]
    public string ServerName { get; set; }

    [Required]
    [MaxLength(100)]
    public string ServerKey { get; set; }

    public int ServerPort { get; set; }

    public bool ServerSsl { get; set; }
        
    public ICollection<UserEmails> UserEmails { get; set; } = new HashSet<UserEmails>();

    public ICollection<EmailsHistory> EmailsHistory { get; set; } = new HashSet<EmailsHistory>();
}