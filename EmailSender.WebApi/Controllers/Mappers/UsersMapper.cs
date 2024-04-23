using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Application.Users.Commands;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers.Mappers;

/// <summary>
/// Users controller mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class UsersMapper
{
    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static GeneratePrivateKeyCommand MapToGeneratePrivateKeyCommand(GeneratePrivateKeyDto model) => new()
    {
        UserId = model.UserId
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AddUserCommand MapToAddUserCommand(AddUserDto model) => new()
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AlterUserStatusCommand MapToAlterUserStatusCommand(AlterUserStatusDto model) => new()
    {
        UserId = model.UserId,
        Status = model.Status
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateUserCommand MapToUpdateUserCommand(UpdateUserDto model) => new()
    {
        UserId = model.UserId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
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

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveUserCommand MapToRemoveUserCommand(RemoveUserDto model) => new()
    {
        UserId = model.UserId,
        SoftDelete = model.SoftDelete
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AddUserEmailCommand MapToAddUserEmailCommand(AddUserEmailDto model) => new()
    {
        UserId = model.UserId,
        EmailId = model.EmailId
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateUserEmailCommand MapToUpdateUserEmailCommand(UpdateUserEmailDto model) => new()
    {
        UserId = model.UserId,
        OldEmailId = model.OldEmailId,
        NewEmailId = model.NewEmailId
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveUserEmailCommand MapToRemoveUserEmailCommand(RemoveUserEmailDto model) => new()
    {
        UserId = model.UserId,
        EmailId = model.EmailId
    };
}