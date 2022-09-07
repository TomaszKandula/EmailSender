namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class GeneratePrivateKeyCommand : IRequest<string>
{
    public Guid? UserId { get; set; }
}