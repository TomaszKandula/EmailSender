namespace EmailSender.Backend.Shared.Dto;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SendEmailDto
{
    public string PrivateKey { get; set; }

    public string From { get; set; }
        
    public List<string> To { get; set; }
        
    public List<string> Cc { get; set; }
        
    public List<string> Bcc { get; set; }

    public string Subject { get; set; }
        
    public string Body { get; set; }
        
    public bool IsHtml { get; set; }
}