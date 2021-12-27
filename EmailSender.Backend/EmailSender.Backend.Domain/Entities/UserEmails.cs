namespace EmailSender.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserEmails : Entity<Guid>
{
    public Guid UserId { get; set; }
        
    public Guid EmailId { get; set; }

    public Users Users { get; set; }

    public Emails Emails { get; set; }
}