namespace EmailSender.Backend.AppService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetUserDetailsValidator : AbstractValidator<GetUserDetailsRequest>
    {
        public GetUserDetailsValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}