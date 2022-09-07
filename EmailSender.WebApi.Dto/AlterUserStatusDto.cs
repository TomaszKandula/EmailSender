using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.WebApi.Dto;

[ExcludeFromCodeCoverage]
public class AlterUserStatusDto
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}