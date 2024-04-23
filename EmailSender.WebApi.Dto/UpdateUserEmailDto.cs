using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to change user address.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserEmailDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Previous email address ID.
    /// </summary>
    public Guid OldEmailId { get; set; }

    /// <summary>
    /// New email address ID.
    /// </summary>
    public Guid NewEmailId { get; set; }
}