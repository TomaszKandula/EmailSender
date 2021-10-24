namespace EmailSenderService.WebApi.Validators
{
    using FluentValidation;
    using Backend.Shared.Dto;
    using Backend.Shared.Resources;

    public class SendEmailDtoValidator : AbstractValidator<SendEmailDto>
    {
        public SendEmailDtoValidator()
        {
            RuleFor(dto => dto.From)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);

            RuleFor(dto => dto.Subject)
                .NotEmpty()
                .WithErrorCode(nameof(ValidationCodes.REQUIRED))
                .WithMessage(ValidationCodes.REQUIRED);
        }
    }
}