using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class UpdateUserEmailDto
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}