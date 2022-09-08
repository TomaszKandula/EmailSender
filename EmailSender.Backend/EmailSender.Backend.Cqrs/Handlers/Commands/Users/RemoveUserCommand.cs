using System;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class RemoveUserCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}