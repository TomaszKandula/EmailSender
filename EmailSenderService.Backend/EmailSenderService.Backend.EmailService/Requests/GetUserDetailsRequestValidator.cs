namespace EmailSenderService.Backend.EmailService.Requests
{
    using FluentValidation;
    using Shared.Resources;

    public class GetUserDetailsRequestValidator : AbstractValidator<GetUserDetailsRequest>
    {
        public GetUserDetailsRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}