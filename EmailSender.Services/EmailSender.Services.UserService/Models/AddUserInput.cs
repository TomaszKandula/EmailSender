using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class AddUserInput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}