namespace EmailSender.Backend.EmailService.Requests
{
    using FluentValidation;
    using Shared.Resources;

    public class GetAllowDomainsRequestValidator : AbstractValidator<GetAllowDomainsRequest>
    {
        public GetAllowDomainsRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}