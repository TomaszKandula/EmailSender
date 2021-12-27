namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings;

using Shared.Resources;
using FluentValidation;

public class GetUserBillingQueryValidator : AbstractValidator<GetUserBillingQuery>
{
    public GetUserBillingQueryValidator()
    {
        RuleFor(request => request.PrivateKey)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);

        RuleFor(request => request.BillingId)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}