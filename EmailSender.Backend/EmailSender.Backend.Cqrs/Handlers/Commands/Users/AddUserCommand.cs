namespace EmailSender.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;

public class AddUserCommand : IRequest<AddUserCommandResult>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }
}