using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Users.Commands;

public class RemoveUserEmailCommandValidator : AbstractValidator<RemoveUserEmailCommand>
{
    public RemoveUserEmailCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.EmailId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}