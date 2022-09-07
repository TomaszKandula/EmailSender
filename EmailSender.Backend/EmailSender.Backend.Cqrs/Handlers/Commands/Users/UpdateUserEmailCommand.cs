namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using MediatR;

public class UpdateUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}