namespace EmailSender.Backend.Cqrs.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQueryRequest>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}