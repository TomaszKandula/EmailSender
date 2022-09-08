using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Cqrs.Handlers.Commands.Emails;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers.Mappers;

[ExcludeFromCodeCoverage]
public static class EmailMapper
{
    public static SendEmailCommand MapToSendEmailRequest(SendEmailDto model) => new ()
    {
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
        Emails = model.Emails
    };
}