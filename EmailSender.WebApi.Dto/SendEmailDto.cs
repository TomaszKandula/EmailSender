using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to send an email.
/// </summary>
[ExcludeFromCodeCoverage]
public class SendEmailDto
{
    /// <summary>
    /// Email address to be sent from.
    /// </summary>
    public string From { get; set; } = "";

    /// <summary>
    /// Email address to be sent to.
    /// </summary>
    public List<string> To { get; set; } = new();

    /// <summary>
    /// Carbon copy list.
    /// </summary>
    public List<string>? Cc { get; set; }

    /// <summary>
    /// Blind carbon copy list.
    /// </summary>
    public List<string>? Bcc { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Email body.
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// Indicating if email body is HTML or plain text.
    /// </summary>
    public bool IsHtml { get; set; }
}