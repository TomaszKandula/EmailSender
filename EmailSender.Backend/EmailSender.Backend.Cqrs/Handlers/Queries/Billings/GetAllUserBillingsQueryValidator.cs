namespace EmailSender.Backend.Cqrs.Handlers.Queries.Billings
{
    using FluentValidation;
    using Shared.Resources;

    public class GetAllUserBillingsQueryValidator : AbstractValidator<GetAllUserBillingsQuery>
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