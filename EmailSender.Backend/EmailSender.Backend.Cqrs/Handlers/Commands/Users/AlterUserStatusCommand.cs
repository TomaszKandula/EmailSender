namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using System;
using Domain.Enums;
using MediatR;

public class AlterUserStatusCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public UserStatus Status { get; set; }
}