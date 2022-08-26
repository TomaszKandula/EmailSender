namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;
using System;

public class AddUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}