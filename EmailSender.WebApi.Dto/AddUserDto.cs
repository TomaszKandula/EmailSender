using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to register new user.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserDto
{
    /// <summary>
    /// Name.
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Surname.
    /// </summary>
    public string LastName { get; set; } = "";

    /// <summary>
    /// Email address.
    /// </summary>
    public string EmailAddress { get; set; } = "";
}