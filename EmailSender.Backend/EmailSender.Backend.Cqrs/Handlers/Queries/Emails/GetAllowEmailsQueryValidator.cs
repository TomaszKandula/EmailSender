namespace EmailSender.Backend.Cqrs.Handlers.Queries.Emails
{
    using FluentValidation;
    using Shared.Resources;

    public class GetAllowEmailsQueryValidator : AbstractValidator<GetAllowEmailsQuery>
    {
        public GetAllowEmailsQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}