using System.Diagnostics.CodeAnalysis;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class AddUserEmailDto
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}