namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails;

using FluentValidation;
using Shared.Resources;

public class GetEmailsHistoryQueryValidator : AbstractValidator<GetEmailsHistoryQuery>
{
    public GetEmailsHistoryQueryValidator()
    {
        RuleFor(request => request.PrivateKey)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}