namespace EmailSender.Backend.Cqrs.Mappers;

using Shared.Dto;
using System.Diagnostics.CodeAnalysis;
using Handlers.Commands.Emails;

[ExcludeFromCodeCoverage]
public static class EmailMapper
{
    public static SendEmailCommand MapToSendEmailRequest(SendEmailDto model) => new ()
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

    public static VerifyEmailCommand MapToVerifyEmailRequest(VerifyEmailDto model) => new()
    {
        PrivateKey = model.PrivateKey,
        Emails = model.Emails
    };
}