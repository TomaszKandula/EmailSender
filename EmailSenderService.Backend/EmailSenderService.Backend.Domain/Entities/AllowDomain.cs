namespace EmailSenderService.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class AllowDomain : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Host { get; set; }

        public User User { get; set; }
    }
}