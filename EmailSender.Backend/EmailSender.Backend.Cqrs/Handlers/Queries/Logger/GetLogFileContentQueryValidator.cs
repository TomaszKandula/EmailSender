namespace EmailSender.Backend.Cqrs.Handlers.Queries.Logger;

using FluentValidation;
using Shared.Resources;

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