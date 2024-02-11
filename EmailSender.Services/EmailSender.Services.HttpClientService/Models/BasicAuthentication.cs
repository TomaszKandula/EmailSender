using System.Diagnostics.CodeAnalysis;
using EmailSender.Services.HttpClientService.Abstractions;

namespace EmailSender.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class BasicAuthentication : IAuthentication
{
    public string Login { get; set; } = "";

    public string Password { get; set; } = "";
}