namespace EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;

using FluentValidation;
using Shared.Resources;

public class GetServerStatusQueryValidator : AbstractValidator<GetServerStatusQuery>
{
    public GetServerStatusQueryValidator()
    {
        RuleFor(request => request.EmailAddress)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .EmailAddress()
            .WithErrorCode(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS))
            .WithMessage(ValidationCodes.INVALID_EMAIL_ADDRESS);
    }
}