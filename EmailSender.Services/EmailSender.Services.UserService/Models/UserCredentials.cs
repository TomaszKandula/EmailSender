namespace EmailSender.Services.UserService.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserCredentials
{
    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }
}