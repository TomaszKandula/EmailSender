using System.Diagnostics.CodeAnalysis;
using EmailSender.Services.HttpClientService.Abstractions;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class ContentDictionary : IPayloadContent
{
    public IDictionary<string, string> Payload { get; set; } = new Dictionary<string, string>();
}