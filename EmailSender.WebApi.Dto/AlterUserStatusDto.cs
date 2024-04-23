using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to change use status.
/// </summary>
[ExcludeFromCodeCoverage]
public class AlterUserStatusDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Desired user status.
    /// </summary>
    public UserStatus Status { get; set; }
}