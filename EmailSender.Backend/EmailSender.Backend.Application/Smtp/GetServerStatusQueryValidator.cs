using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Handlers.Queries.Smtp;

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