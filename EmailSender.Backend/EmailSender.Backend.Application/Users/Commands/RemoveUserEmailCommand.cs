using MediatR;
using System;

namespace EmailSender.Backend.Application.Users.Commands;

public class RemoveUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}