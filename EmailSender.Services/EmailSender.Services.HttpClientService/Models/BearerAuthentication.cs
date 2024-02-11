using System.Diagnostics.CodeAnalysis;
using EmailSender.Services.HttpClientService.Abstractions;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class BearerAuthentication : IAuthentication
{
    public string Token { get; set; } = "";
}