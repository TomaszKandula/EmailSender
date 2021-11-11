namespace EmailSender.Backend.Cqrs.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetAllowDomainsQueryValidator : AbstractValidator<GetAllowDomainsQueryRequest>
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