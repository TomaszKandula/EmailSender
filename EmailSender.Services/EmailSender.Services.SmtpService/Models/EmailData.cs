using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.SmtpService.Models;

[ExcludeFromCodeCoverage]
public class EmailData
{
    public string From { get; set; } = "";

    public List<string> To { get; set; } = new();
        
    public List<string> Cc { get; set; } = new();
        
    public List<string> Bcc { get; set; } = new();

    public string Subject { get; set; } = "";

    public string PlainText { get; set; } = "";

    public string HtmlBody { get; set; } = "";
}