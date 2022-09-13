using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class AddUserOutput
{
    public Guid UserId { get; set; }

    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }

    public UserStatus Status { get; set; }
}