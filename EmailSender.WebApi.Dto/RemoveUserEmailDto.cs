using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to remove user email.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserEmailDto
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