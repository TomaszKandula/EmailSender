namespace EmailSender.Services.SenderService.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class ErrorResult
{
    public string ErrorCode { get; set; }
        
    public string ErrorDesc { get; set; }
        
    public string InnerMessage { get; set; }
}