using System;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class UpdateUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}