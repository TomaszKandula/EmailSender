using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to generate new PK.
/// </summary>
[ExcludeFromCodeCoverage]
public class GeneratePrivateKeyDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }
}