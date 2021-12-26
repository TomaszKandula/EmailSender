namespace EmailSender.Backend.Cqrs.Validators
{
    using Requests;
    using Shared.Resources;
    using FluentValidation;

    public class GetUserBillingQueryValidator : AbstractValidator<GetUserBillingQueryRequest>
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
}