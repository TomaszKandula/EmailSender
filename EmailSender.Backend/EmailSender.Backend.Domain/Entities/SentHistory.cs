using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class SentHistory : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid EmailId { get; set; }

    public DateTime SentAt { get; set; }

    public Users Users { get; set; }

    public Emails Emails { get; set; }
}