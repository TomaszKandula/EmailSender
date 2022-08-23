namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;
using System;

public class RemoveUserEmailCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }

    public Guid EmailId { get; set; }
}