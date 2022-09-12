using System;
using MediatR;

namespace EmailSender.Backend.Application.Users.Commands;

public class RemoveUserCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public bool SoftDelete { get; set; }
}