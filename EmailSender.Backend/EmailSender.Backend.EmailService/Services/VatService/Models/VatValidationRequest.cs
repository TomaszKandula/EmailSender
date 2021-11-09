namespace EmailSender.Backend.EmailService.Services.VatService.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class VatValidationRequest
    {
        public string VatNumber { get; set; }

        public IEnumerable<VatNumberPattern> VatNumberPatterns { get; set; }

        public bool CalculateCheckSum { get; set; } 

        public bool CheckZeros { get; set; }

        public VatValidationRequest(string vatNumber, IEnumerable<VatNumberPattern> vatNumberPatterns, bool calculateCheckSum, bool checkZeros)
        {
            VatNumber = vatNumber;
            VatNumberPatterns = vatNumberPatterns;
            CalculateCheckSum = calculateCheckSum;
            CheckZeros = checkZeros;
        }
    }
}