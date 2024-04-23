using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

/// <summary>
/// Use it when you want to verify given email addresses.
/// </summary>
[ExcludeFromCodeCoverage]
public class VerifyEmailDto
{
    /// <summary>
    /// Emails to be verified.
    /// </summary>
    public List<string> Emails { get; set; } = new();
}