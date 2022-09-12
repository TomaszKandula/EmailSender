using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Users.Commands;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.EmailAddress)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .EmailAddress()
            .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
            .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
    }
}