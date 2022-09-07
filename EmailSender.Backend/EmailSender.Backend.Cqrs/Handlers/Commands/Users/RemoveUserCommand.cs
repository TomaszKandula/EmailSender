namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class RemoveUserCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}