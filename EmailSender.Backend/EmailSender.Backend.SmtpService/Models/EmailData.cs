namespace EmailSender.Backend.SmtpService.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class EmailData
{
    public string From { get; set; }

    public List<string> To { get; set; }
        
    public List<string> Cc { get; set; }
        
    public List<string> Bcc { get; set; }
        
    public string Subject { get; set; }
        
    public string PlainText { get; set; }
        
    public string HtmlBody { get; set; }
}