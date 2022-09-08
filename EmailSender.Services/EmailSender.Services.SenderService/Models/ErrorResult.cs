using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.SenderService.Models;

[ExcludeFromCodeCoverage]
public class ErrorResult
{
    public string ErrorCode { get; set; }
        
    public string ErrorDesc { get; set; }
        
    public string InnerMessage { get; set; }
}