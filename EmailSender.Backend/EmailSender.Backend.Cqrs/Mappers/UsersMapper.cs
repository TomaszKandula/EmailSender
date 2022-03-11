namespace EmailSender.Backend.Cqrs.Mappers;

using Handlers.Commands.Users;
using Shared.Dto;

public static class UsersMapper
{
    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new()
    {
        FirstName = model.FirstName,
        LastName = model.FirstName,
        EmailAddress = model.EmailAddress
    };
}