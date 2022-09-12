using System;
using EmailSender.Backend.Domain.Enums;
using MediatR;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class AlterUserStatusCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}