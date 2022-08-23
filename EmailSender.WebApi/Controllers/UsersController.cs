namespace EmailSender.WebApi.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Shared.Dto;
using Backend.Cqrs.Mappers;
using Backend.Cqrs.Handlers.Queries.Users;
using Backend.Cqrs.Handlers.Commands.Users;
using MediatR;

[ApiVersion("1.0")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType(typeof(GetUserDetailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserDetailsQueryResult> GetUserDetails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserDetailsQuery());

    [HttpGet]
    [ProducesResponseType(typeof(GetUserAllowedIpsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserAllowedIpsQueryResult> GetUserAllowedIps([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserAllowedIpsQuery());

    [HttpGet]
    [ProducesResponseType(typeof(GetUserEmailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserEmailsQueryResult> GetUserEmails([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetUserEmailsQuery());

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AddUserCommandResult), StatusCodes.Status200OK)]
    public async Task<AddUserCommandResult> AddUser([FromBody] AddUserDto payload) 
        => await Mediator.Send(UsersMapper.MapToAddUserCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUser([FromBody] UpdateUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserDetails([FromBody] UpdateUserDetailsDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserDetailsCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUser([FromBody] RemoveUserDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> AddUserEmail([FromBody] AddUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToAddUserEmailCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateUserEmail([FromBody] UpdateUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToUpdateUserEmailCommand(payload));

    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveUserEmail([FromBody] RemoveUserEmailDto payload, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(UsersMapper.MapToRemoveUserEmailCommand(payload));
}