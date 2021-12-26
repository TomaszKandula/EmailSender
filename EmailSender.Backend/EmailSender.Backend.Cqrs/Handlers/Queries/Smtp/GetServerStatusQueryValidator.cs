namespace EmailSender.Backend.Cqrs.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetServerStatusQueryValidator : AbstractValidator<GetServerStatusQueryRequest>
    {
        public GetServerStatusQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(request => request.EmailAddress)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
        }
    }
}