namespace EmailSender.Backend.AppService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetAllowDomainsValidator : AbstractValidator<GetAllowDomainsRequest>
    {
        public GetAllowDomainsValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}