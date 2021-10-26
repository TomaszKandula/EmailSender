namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AllowEmail : Entity<Guid>
    {
        public Guid UserId { get; set; }
        
        public Guid EmailId { get; set; }

        public User User { get; set; }

        public Email Email { get; set; }
    }
}