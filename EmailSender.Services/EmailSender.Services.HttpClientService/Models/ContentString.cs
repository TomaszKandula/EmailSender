using System.Diagnostics.CodeAnalysis;
using EmailSender.Services.HttpClientService.Abstractions;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class ContentString : IPayloadContent
{
    public object? Payload { get; set; }
}