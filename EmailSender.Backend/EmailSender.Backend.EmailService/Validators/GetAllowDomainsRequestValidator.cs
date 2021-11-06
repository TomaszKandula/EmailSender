namespace EmailSender.Backend.EmailService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetAllowDomainsRequestValidator : AbstractValidator<GetAllowDomainsRequest>
    {
        public GetAllowDomainsRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}