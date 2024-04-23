using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to update user data.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

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