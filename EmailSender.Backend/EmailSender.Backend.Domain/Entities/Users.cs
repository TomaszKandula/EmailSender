namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Users : Entity<Guid>
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

        public ICollection<EmailHistory> EmailHistory { get; set; } = new HashSet<EmailHistory>();

        public ICollection<AllowDomain> AllowDomain { get; set; } = new HashSet<AllowDomain>();

        public ICollection<Pricing> Pricing { get; set; } = new HashSet<Pricing>();

        public ICollection<Billing> Billing { get; set; } = new HashSet<Billing>();

        public ICollection<RequestsHistory> RequestsHistory { get; set; } = new HashSet<RequestsHistory>();

        public ICollection<UserDetails> UserDetails { get; set; } = new HashSet<UserDetails>();
    }
}