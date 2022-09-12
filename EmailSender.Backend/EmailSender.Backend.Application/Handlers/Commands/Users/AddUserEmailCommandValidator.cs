using System;
using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class AddUserEmailCommandValidator : AbstractValidator<AddUserEmailCommand>
{
    public AddUserEmailCommandValidator()
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