namespace EmailSenderService.Backend.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class RegisteredEmail : Entity<Guid>
    {
        public Guid UserId { get; set; }
        
        public Guid EmailId { get; set; }

        public User User { get; set; }

        public Email Email { get; set; }
    }
}