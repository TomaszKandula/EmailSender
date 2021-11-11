namespace EmailSender.Backend.Cqrs.Validators
{
    using Requests;
    using Shared.Resources;
    using FluentValidation;

    public class GetUserBillingValidator : AbstractValidator<GetUserBillingRequest>
    {
        public GetUserBillingValidator()
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
}