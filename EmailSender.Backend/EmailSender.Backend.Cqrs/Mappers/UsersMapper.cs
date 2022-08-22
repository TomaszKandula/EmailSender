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

    public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto model) => new()
    {
        UserId = model.UserId,
        FirstName = model.FirstName,
        LastName = model.FirstName,
        EmailAddress = model.EmailAddress
    };

    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        UserId = model.UserId,
        SoftDelete = model.SoftDelete
    };
}