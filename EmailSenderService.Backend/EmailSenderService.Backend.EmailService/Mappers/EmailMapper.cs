namespace EmailSenderService.Backend.EmailService.Mappers
{
    using Requests;
    using Shared.Dto;

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
    }
}