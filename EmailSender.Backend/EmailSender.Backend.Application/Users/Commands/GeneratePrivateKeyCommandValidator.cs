using System;
using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class GeneratePrivateKeyCommandValidator : AbstractValidator<GeneratePrivateKeyCommand>
{
    public GeneratePrivateKeyCommandValidator()
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