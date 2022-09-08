using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class GeneratePrivateKeyDto
{
    public Guid? UserId { get; set; }
}