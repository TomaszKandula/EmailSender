namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class RequestHistory : Entity<Guid>
    {
        public Guid UserId { get; set; }

        [Required]
        public DateTime Requested { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string RequestName { get; set; }

        public Users Users { get; set; }
    }
}