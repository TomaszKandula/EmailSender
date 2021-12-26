namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using FluentValidation;
    using Shared.Resources;

    public class GetSentHistoryQueryValidator : AbstractValidator<GetSentHistoryQuery>
    {
        public GetSentHistoryQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}