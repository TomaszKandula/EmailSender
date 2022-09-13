using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class AlterUserStatusInput
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}