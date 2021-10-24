namespace EmailSenderService.Backend.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Email : Entity<Guid>
    {
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public ICollection<AllowEmail> AllowEmail { get; set; } = new HashSet<AllowEmail>();

        public ICollection<History> History { get; set; } = new HashSet<History>();
    }
}