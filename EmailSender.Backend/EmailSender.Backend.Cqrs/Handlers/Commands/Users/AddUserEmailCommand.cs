using MediatR;
using System;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class AddUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}