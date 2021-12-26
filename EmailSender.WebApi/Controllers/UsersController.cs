namespace EmailSender.WebApi.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Cqrs.Handlers.Queries.Users;
using MediatR;

[ApiVersion("1.0")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType(typeof(GetUserDetailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserDetailsQueryResult> GetUserDetails([FromQuery] string key) 
        => await Mediator.Send(new GetUserDetailsQuery { PrivateKey = key });

    [HttpGet]
    [ProducesResponseType(typeof(GetUserDomainsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserDomainsQueryResult> GetUserDomains([FromQuery] string key) 
        => await Mediator.Send(new GetUserDomainsQuery { PrivateKey = key });

    [HttpGet]
    [ProducesResponseType(typeof(GetUserEmailsQueryResult), StatusCodes.Status200OK)]
    public async Task<GetUserEmailsQueryResult> GetUserEmails([FromQuery] string key) 
        => await Mediator.Send(new GetUserEmailsQuery { PrivateKey = key });
}