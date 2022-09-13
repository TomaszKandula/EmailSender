using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class SendEmailDto
{
    public string From { get; set; } = "";

    public List<string> To { get; set; } = new();
        
    public List<string>? Cc { get; set; }
        
    public List<string>? Bcc { get; set; }

    public string Subject { get; set; } = "";
        
    public string Body { get; set; } = "";
        
    public bool IsHtml { get; set; }
}