namespace EmailSender.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SentHistory : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid EmailId { get; set; }

    public DateTime Sent { get; set; }

    public Users Users { get; set; }

    public Emails Emails { get; set; }
}