using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Emails;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(command => command.From)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .EmailAddress()
            .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
            .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

        RuleFor(command => command.Subject)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.To)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleForEach(command => command.To)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .EmailAddress()
            .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
            .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);

        When(command => command.Cc != null, () =>
        {
            RuleFor(command => command.Cc)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleForEach(command => command.Cc)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
        });

        When(command => command.Bcc != null, () =>
        {
            RuleFor(command => command.Bcc)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleForEach(command => command.Bcc)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED)
                .EmailAddress()
                .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
                .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
        });
    }
}