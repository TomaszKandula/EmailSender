using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class VerifyEmailDto
{
    public List<string> Emails { get; set; } = new();
}