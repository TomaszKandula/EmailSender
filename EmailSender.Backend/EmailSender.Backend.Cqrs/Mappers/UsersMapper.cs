namespace EmailSender.Backend.Cqrs.Mappers;

using Handlers.Commands.Users;
using Shared.Dto;

public static class UsersMapper
{
    public static GeneratePrivateKeyCommand MapToGeneratePrivateKeyCommand(GeneratePrivateKeyDto model) => new()
    {
        UserId = model.UserId
    };

    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new()
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress
    };

    public static AlterUserStatusCommand MapToAlterUserStatusCommand(AlterUserStatusDto model) => new()
    {
        UserId = model.UserId,
        Status = model.Status
    };

    public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto model) => new()
    {
        UserId = model.UserId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress
    };

    public static UpdateUserDetailsCommand MapToUpdateUserDetailsCommand(UpdateUserDetailsDto model) => new()
    {
        UserId = model.UserId,
        CompanyName = model.CompanyName,
        VatNumber = model.VatNumber,
        StreetAddress = model.StreetAddress,
        PostalCode = model.PostalCode,
        Country = model.Country,
        City = model.City
    };

    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        UserId = model.UserId,
        SoftDelete = model.SoftDelete
    };

    public static AddUserEmailCommand MapToAddUserEmailCommand(AddUserEmailDto model) => new()
    {
        UserId = model.UserId,
        EmailId = model.EmailId
    };

    public static UpdateUserEmailCommand MapToUpdateUserEmailCommand(UpdateUserEmailDto model) => new()
    {
        UserId = model.UserId,
        OldEmailId = model.OldEmailId,
        NewEmailId = model.NewEmailId
    };

    public static RemoveUserEmailCommand MapToRemoveUserEmailCommand(RemoveUserEmailDto model) => new()
    {
        UserId = model.UserId,
        EmailId = model.EmailId
    };
}