namespace EmailSender.Backend.EmailService.Services.VatService.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class PolishVatNumberOptions
    {
        public bool CalculateCheckSum { get; set; }

        public bool CheckZeros { get; set; }

        public PolishVatNumberOptions(bool checkZeros, bool calculateCheckSum)
        {
            CheckZeros = checkZeros;
            CalculateCheckSum = calculateCheckSum;
        }
    }
}