namespace EmailSender.Backend.Cqrs.Mappers
{
    using Shared.Dto;
    using System.Diagnostics.CodeAnalysis;
    using Requests;

    [ExcludeFromCodeCoverage]
    public static class EmailMapper
    {
        public static SendEmailRequest MapToSendEmailRequest(SendEmailDto model) => new ()
        {
            PrivateKey = model.PrivateKey,
            From = model.From,
            To = model.To,
            Cc = model.Cc,
            Bcc = model.Bcc,
            Subject = model.Subject,
            Body = model.Body,
            IsHtml = model.IsHtml
        };

        public static VerifyEmailRequest MapToVerifyEmailRequest(VerifyEmailDto model) => new()
        {
            PrivateKey = model.PrivateKey,
            Emails = model.Emails
        };
    }
}