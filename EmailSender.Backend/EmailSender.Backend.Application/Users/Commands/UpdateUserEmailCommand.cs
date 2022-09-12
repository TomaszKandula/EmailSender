using System;
using MediatR;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class UpdateUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid OldEmailId { get; set; }

    public Guid NewEmailId { get; set; }
}