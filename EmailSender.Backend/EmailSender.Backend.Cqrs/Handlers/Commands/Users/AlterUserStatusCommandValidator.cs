namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using FluentValidation;
using Shared.Resources;

public class AlterUserStatusCommandValidator : AbstractValidator<AlterUserStatusCommand>
{
    public AlterUserStatusCommandValidator()
    {
        When(command => command.UserId != null, () =>
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty)
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        });
    }
}