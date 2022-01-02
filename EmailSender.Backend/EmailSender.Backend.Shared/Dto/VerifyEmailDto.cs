namespace EmailSender.Backend.Shared.Dto;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class VerifyEmailDto
{
    public IEnumerable<string> Emails { get; set; }
}