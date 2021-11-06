namespace EmailSender.Backend.EmailService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class SendEmailRequestValidator : AbstractValidator<SendEmailRequest>
    {
        public SendEmailRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(request => request.From)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

            RuleFor(request => request.Subject)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(request => request.To)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleForEach(request => request.To)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

            When(request => request.Cc != null, () =>
            {
                RuleFor(request => request.Cc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);

                RuleForEach(request => request.Cc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .EmailAddress()
                    .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                    .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
            });

            When(request => request.Bcc != null, () =>
            {
                RuleFor(request => request.Bcc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);

                RuleForEach(request => request.Bcc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .EmailAddress()
                    .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                    .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
            });
        }
    }
}