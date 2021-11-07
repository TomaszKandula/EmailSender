namespace EmailSender.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class VatNumberPattern : Entity<Guid>
    {
        [Required]
        [MaxLength(2)]
        public string CountryCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Pattern { get; set; }
    }
}