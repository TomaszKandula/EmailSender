namespace EmailSender.Backend.EmailService.Services.VatService
{
    using FluentValidation.Results;
    using Models;

    public interface IVatService
    {
        ValidationResult ValidateVatNumber(VatValidationRequest request);
    }
}