namespace EmailSender.Backend.Cqrs.Validators
{
    using Requests;
    using FluentValidation;
    using Shared.Resources;

    public class GetAllUserBillingsQueryValidator : AbstractValidator<GetAllUserBillingsQueryRequest>
    {
        public GetAllUserBillingsQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}