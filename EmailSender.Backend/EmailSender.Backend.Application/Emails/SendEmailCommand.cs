using System.Collections.Generic;
using MediatR;

namespace EmailSender.Backend.Application.Emails;

public class SendEmailCommand : IRequest<Unit>
{
    public string From { get; set; }
        
    public List<string> To { get; set; }
        
    public List<string> Cc { get; set; }
        
    public List<string> Bcc { get; set; }

    public string Subject { get; set; }
        
    public string Body { get; set; }
        
    public bool IsHtml { get; set; }
}