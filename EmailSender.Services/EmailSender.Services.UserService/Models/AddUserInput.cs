namespace EmailSender.Services.UserService.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddUserInput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}