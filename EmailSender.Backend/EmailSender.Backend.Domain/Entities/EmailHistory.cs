namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class EmailHistory : Entity<Guid>
    {
        public Guid UserId { get; set; }

        public Guid EmailId { get; set; }

        public DateTime Sent { get; set; }

        public Users Users { get; set; }

        public Email Email { get; set; }
    }
}