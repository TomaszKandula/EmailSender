using MediatR;

namespace EmailSender.Backend.Application.Users.Commands;

public class AddUserCommand : IRequest<AddUserCommandResult>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}