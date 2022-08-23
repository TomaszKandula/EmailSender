namespace EmailSender.Backend.Shared.Dto;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddUserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}