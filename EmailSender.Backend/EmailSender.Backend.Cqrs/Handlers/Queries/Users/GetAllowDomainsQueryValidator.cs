namespace EmailSender.Backend.Cqrs.Handlers.Queries.Users
{
    using FluentValidation;
    using Shared.Resources;

    public class GetAllowDomainsQueryValidator : AbstractValidator<GetAllowDomainsQuery>
    {
        public GetAllowDomainsQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}