using System;
using MediatR;

namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

public class UpdateUserCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}