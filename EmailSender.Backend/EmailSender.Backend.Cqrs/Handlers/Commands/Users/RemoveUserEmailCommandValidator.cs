namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using FluentValidation;
using Shared.Resources;

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