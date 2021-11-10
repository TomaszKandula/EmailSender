namespace EmailSender.Backend.AppService.Validators
{
    using FluentValidation;
    using Shared.Resources;
    using Requests;

    public class GetSentHistoryValidator : AbstractValidator<GetSentHistoryRequest>
    {
        public GetSentHistoryValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}