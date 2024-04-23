using EmailSender.Backend.Application.Users.Commands;
using EmailSender.Backend.Application.Users.Queries;
using EmailSender.Backend.Shared.Attributes;
using EmailSender.WebApi.Controllers.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// Endpoint for managing users.
/// </summary>
[ApiVersion("1.0")]
public class UsersController : ApiBaseController
{
    ///<inheritdoc />
    public UsersController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns user details.
    /// </summary>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Object w/data.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserDetailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserDetailsQueryResult> GetUserDetails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserDetailsQuery());

    /// <summary>
    /// Returns allowed IP for given user.
    /// </summary>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Object w/data.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserAllowedIpsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserAllowedIpsQueryResult> GetUserAllowedIps([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserAllowedIpsQuery());

    /// <summary>
    /// Returns all teh user registered email addresses.
    /// </summary>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Object w/data.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserEmailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserEmailsQueryResult> GetUserEmails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserEmailsQuery());

    /// <summary>
    /// Returns private key for given user.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>String.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<string> GeneratePrivateKey([FromBody] GeneratePrivateKeyDto payload, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(UsersMapper.MapToGeneratePrivateKeyCommand(payload));

    /// <summary>
    /// Adds new user to the system.
    /// </summary>
    /// <param name="payload">Input data.</param>
    /// <returns>Object w/data.</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AddUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AddUserCommandResult> AddUser([FromBody] AddUserDto payload) 
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payload));

    /// <summary>
    /// Alters user status.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [RequireAdministrator]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> AlterUserStatus([FromBody] AlterUserStatusDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToAlterUserStatusCommand(payload));

    /// <summary>
    /// Updates user data such as name and email address.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payload));

    /// <summary>
    /// Updates user details such as Company Name, VAT address etc.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserDetails([FromBody] UpdateUserDetailsDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserDetailsCommand(payload));

    /// <summary>
    /// Removes user from database.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payload));

    /// <summary>
    /// Adds user email address.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> AddUserEmail([FromBody] AddUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToAddUserEmailCommand(payload));

    /// <summary>
    /// Updates user email.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserEmail([FromBody] UpdateUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserEmailCommand(payload));

    /// <summary>
    /// Removes user email.
    /// </summary>
    /// <param name="payload">Data.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserEmail([FromBody] RemoveUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserEmailCommand(payload));
}