namespace EmailSender.Services.SenderService.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Configuration
{
    public string From { get; set; }
        
    public List<string> To { get; set; }
        
    public List<string> Cc { get; set; }
        
    public List<string> Bcc { get; set; }

    public string Subject { get; set; }
        
    public string Body { get; set; }
        
    public bool IsHtml { get; set; }
}