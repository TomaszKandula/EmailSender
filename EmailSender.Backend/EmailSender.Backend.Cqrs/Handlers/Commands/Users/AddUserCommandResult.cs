namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;

public class AddUserCommandResult
{
    public Guid UserId { get; set; }

    public string PrivateKey { get; set; }

    public string UserAlias { get; set; }

    public string EmailAddress { get; set; }
}