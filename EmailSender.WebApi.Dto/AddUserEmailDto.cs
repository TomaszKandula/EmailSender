using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to add new user email address.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserEmailDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Email ID.
    /// </summary>
    public Guid EmailId { get; set; }
}