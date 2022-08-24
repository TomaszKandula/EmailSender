namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using Domain.Enums;

public class AddUserCommandResult
{
    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }

    public UserStatus Status { get; set; }
}