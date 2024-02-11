using System.Diagnostics.CodeAnalysis;
using EmailSender.Services.HttpClientService.Abstractions;
using Microsoft.Extensions.Primitives;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class Configuration
{
    public string Url { get; set; } = "";

    public string Method { get; set; } = "";

    public StringValues? Range { get; set; }

    public IDictionary<string, string>? Headers { get; set; }

    public IDictionary<string, string?>? QueryParameters { get; set; }

    public IAuthentication? Authentication { get; set; }

    public IPayloadContent? PayloadContent { get; set; }
}