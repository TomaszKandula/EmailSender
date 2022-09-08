using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class RemoveUserEmailDto
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}