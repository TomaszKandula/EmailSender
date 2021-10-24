namespace EmailSenderService.WebApi.Validators
{
    using FluentValidation;
    using Backend.Shared.Dto;
    using Backend.Shared.Resources;

    public class SendEmailDtoValidator : AbstractValidator<SendEmailDto>
    {
        public SendEmailDtoValidator()
        {
            RuleFor(dto => dto.From)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

            RuleFor(dto => dto.Subject)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(dto => dto.To)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleForEach(dto => dto.To)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

            When(dto => dto.Cc != null, () =>
            {
                RuleFor(dto => dto.Cc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);

                RuleForEach(dto => dto.Cc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED)
                    .EmailAddress()
                    .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                    .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
            });

            When(dto => dto.Bcc != null, () =>
            {
                RuleFor(dto => dto.Bcc)
                    .NotEmpty()
                    .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                    .WithMessage(ValidationCodes.REQUIRED);

                RuleForEach(dto => dto.Bcc)
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