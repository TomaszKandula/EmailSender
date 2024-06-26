using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class HttpContentResult
{
    public MediaTypeHeaderValue? ContentType { get; set; }

    public byte[]? Content { get; set; }
}