using MediatR;
using System;

namespace EmailSender.Backend.Application.Users.Commands;

public class AddUserEmailCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public Guid EmailId { get; set; }
}