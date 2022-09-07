using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class RemoveUserDto
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}