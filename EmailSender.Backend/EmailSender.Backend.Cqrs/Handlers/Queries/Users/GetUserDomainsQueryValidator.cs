namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class GetUserDomainsQueryValidator : AbstractValidator<GetUserDomainsQuery>
    {
        public GetUserDomainsQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}