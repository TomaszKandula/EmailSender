namespace EmailSender.Backend.Cqrs.Validators
{
    using Requests;
    using FluentValidation;
    using Shared.Resources;

    public class GetAllUserBillingsValidator : AbstractValidator<GetAllUserBillingsRequest>
    {
        public GetAllUserBillingsValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}