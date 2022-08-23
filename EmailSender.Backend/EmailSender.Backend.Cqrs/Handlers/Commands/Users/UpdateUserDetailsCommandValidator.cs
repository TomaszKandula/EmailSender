namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using FluentValidation;
using Shared.Resources;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(command => command.CompanyName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);

        RuleFor(command => command.VatNumber)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(25)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);

        RuleFor(command => command.StreetAddress)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);

        RuleFor(command => command.PostalCode)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(12)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);

        RuleFor(command => command.Country)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);

        RuleFor(command => command.City)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);
    }
}