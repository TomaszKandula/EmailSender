namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users;

using FluentValidation;
using Shared.Resources;

public class GetUserEmailsQueryValidator : AbstractValidator<GetUserEmailsQuery>
{
    public GetUserEmailsQueryValidator()
    {
        RuleFor(request => request.PrivateKey)
            .NotEmpty()
            .WithErrorCode(nameof(ValidationCodes.REQUIRED))
            .WithMessage(ValidationCodes.REQUIRED);
    }
}