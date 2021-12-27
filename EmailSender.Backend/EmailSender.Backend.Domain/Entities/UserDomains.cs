namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class UserDomains : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Host { get; set; }

        public Users Users { get; set; }
    }
}