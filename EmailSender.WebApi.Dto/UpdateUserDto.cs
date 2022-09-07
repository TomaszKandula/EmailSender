using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    public Guid? UserId { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string EmailAddress { get; set; } = "";
}