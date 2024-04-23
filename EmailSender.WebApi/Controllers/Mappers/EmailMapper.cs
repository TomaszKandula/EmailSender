using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Application.Emails;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers.Mappers;

/// <summary>
/// Email controller mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class EmailMapper
{
    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
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

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static VerifyEmailCommand MapToVerifyEmailRequest(VerifyEmailDto model) => new()
    {
        Emails = model.Emails
    };
}