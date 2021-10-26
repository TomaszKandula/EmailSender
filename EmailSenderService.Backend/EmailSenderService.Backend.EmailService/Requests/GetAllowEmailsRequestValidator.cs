namespace EmailSenderService.Backend.EmailService.Requests
{
    using FluentValidation;
    using Shared.Resources;

    public class GetAllowEmailsRequestValidator : AbstractValidator<GetAllowEmailsRequest>
    {
        public GetAllowEmailsRequestValidator()
        {
            RuleFor(request => request.PrivateKey)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}