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
/// 
/// </summary>
[ApiVersion("1.0")]
public class UsersController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public UsersController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserDetailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserDetailsQueryResult> GetUserDetails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserDetailsQuery());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserAllowedIpsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserAllowedIpsQueryResult> GetUserAllowedIps([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserAllowedIpsQuery());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserEmailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserEmailsQueryResult> GetUserEmails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserEmailsQuery());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<string> GeneratePrivateKey([FromBody] GeneratePrivateKeyDto payload, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(UsersMapper.MapToGeneratePrivateKeyCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AddUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AddUserCommandResult> AddUser([FromBody] AddUserDto payload) 
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [RequireAdministrator]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> AlterUserStatus([FromBody] AlterUserStatusDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToAlterUserStatusCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserDetails([FromBody] UpdateUserDetailsDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserDetailsCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> AddUserEmail([FromBody] AddUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToAddUserEmailCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserEmail([FromBody] UpdateUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserEmailCommand(payload));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserEmail([FromBody] RemoveUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserEmailCommand(payload));
}