using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to remove user.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveUserDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Enable/Disable soft delete.
    /// </summary>
    public bool SoftDelete { get; set; }
}