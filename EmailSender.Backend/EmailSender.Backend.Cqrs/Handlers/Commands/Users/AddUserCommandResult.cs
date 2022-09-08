using System;
using EmailSender.Backend.Domain.Enums;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class AddUserCommandResult
{
    public Guid UserId { get; set; }

    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }

    public UserStatus Status { get; set; }
}