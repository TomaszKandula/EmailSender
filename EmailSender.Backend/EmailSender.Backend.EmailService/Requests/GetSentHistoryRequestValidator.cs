namespace EmailSender.Backend.EmailService.Requests
{
    using FluentValidation;
    using Shared.Resources;

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