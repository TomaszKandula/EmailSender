using System.Threading.Tasks;
using EmailSender.Backend.Cqrs.Handlers.Queries.Smtp;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace EmailSender.WebApi.Controllers;

[ApiVersion("1.0")]
public class SmtpController : BaseController
{
    public SmtpController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [BillableEndpoint]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> GetServerStatus([FromQuery] string emailAddress, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetServerStatusQuery { EmailAddress = emailAddress });
}