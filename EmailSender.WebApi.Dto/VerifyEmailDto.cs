using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class VerifyEmailDto
{
    public IEnumerable<string> Emails { get; set; } = new List<string>();
}