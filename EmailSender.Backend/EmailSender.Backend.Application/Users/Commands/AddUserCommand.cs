using MediatR;

namespace EmailSender.Backend.Application.Handlers.Commands.Users;

public class AddUserCommand : IRequest<AddUserCommandResult>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}