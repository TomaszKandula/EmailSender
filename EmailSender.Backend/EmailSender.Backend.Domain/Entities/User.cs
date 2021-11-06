namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class User : Entity<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(5)]
        public string UserAlias { get; set; }

        [Required]
        [MaxLength(255)]
        public string EmailAddress { get; set; }

        public bool IsActivated { get; set; }

        public DateTime Registered { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string PrivateKey { get; set; }

        public ICollection<AllowEmail> AllowEmail { get; set; } = new HashSet<AllowEmail>();

        public ICollection<History> History { get; set; } = new HashSet<History>();

        public ICollection<AllowDomain> AllowDomain { get; set; } = new HashSet<AllowDomain>();

        public ICollection<Price> Price { get; set; } = new HashSet<Price>();
    }
}