using EmailSender.Backend.Shared.Resources;
using FluentValidation;

namespace EmailSender.Backend.Application.Handlers.Queries.Logger;

public class GetLogFileContentQueryValidator : AbstractValidator<GetLogFileContentQuery>
{
    public GetLogFileContentQueryValidator()
    {
        RuleFor(query => query.LogFileName)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED)
            .MaximumLength(255)
            .WithErrorCode(nameof(ValidationCodes.VALUE_TOO_LONG))
            .WithMessage(ValidationCodes.VALUE_TOO_LONG);
    }
}