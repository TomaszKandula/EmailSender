namespace EmailSender.Backend.EmailService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetSentHistoryRequestValidator : AbstractValidator<GetSentHistoryRequest>
    {
        public GetSentHistoryRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}