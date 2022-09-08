using System;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class GeneratePrivateKeyCommand : IRequest<string>
{
    public Guid? UserId { get; set; }
}