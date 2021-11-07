namespace EmailSender.Backend.EmailService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetAllowEmailsValidator : AbstractValidator<GetAllowEmailsRequest>
    {
        public GetAllowEmailsValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}